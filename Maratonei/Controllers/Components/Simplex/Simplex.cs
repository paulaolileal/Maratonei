using Maratonei.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Controllers.Components.Simplex {

    /// <summary>
    /// Classe que realiza o calculo do simplex
    /// </summary>
    public class Simplex {

        // Estruturas do simplex
        private Tuple<double, double>[ , ] table;
        private ObjectiveFunction objectiveFunction;
        private List<Restriction> restrictionsList;

        // Arrays para guardar os identificadores das variaveis
        private string[ ] columnPositions;
        private string[ ] linePositions;

        // Linhas e colunas permitidas
        private int PermitedColumn;
        private int PermitedLine;

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="po">Funcao objetiva</param>
        /// <param name="restrictions">Lista de restricoes</param>
        public Simplex( ObjectiveFunction po, List<Restriction> restrictions ) {
            objectiveFunction = po;
            restrictionsList = restrictions;

            table = new Tuple<double, double>[ restrictions.Count( ) + 1, po.Z.Count( ) + 1 ];

            columnPositions = new string[ po.Z.Count( ) + 1 ];
            linePositions = new string[ restrictions.Count( ) + 1 ];
        }

        /// <summary>
        /// Chamada para resolver o simplex
        /// </summary>
        /// <returns>Retorna a estrutura em formato de funcao</returns>
        public ObjectiveFunction Solver() {
            PopuleTable( );
            // Chamada da fase um
            return FaseOne( );
        }

        /// <summary>
        /// Inicia a tabela do simplex, colocando os valores das funcoes
        /// nas devidas posicoes da tabela.
        /// </summary>
        public void PopuleTable() {
            int col = 1;
            table[ 0, 0 ] = new Tuple<double, double>( 0, 0 );

            // Insere os elementos fa funcao objetiva
            columnPositions[ 0 ] = "ml";
            foreach (var element in objectiveFunction.Transform( )) {
                table[ 0, col ] = new Tuple<double, double>( element.Item2, 0 );
                columnPositions[ col ] = element.Item1;
                col++;
            }

            // Insere os elementos das restricoes
            col = 0;
            int lin = 1;
            linePositions[ 0 ] = "f(x)";
            foreach (var restriction in restrictionsList) {
                foreach (var element in restriction.Transform( ( objectiveFunction.Type == ObjectiveFunction.FuncType.Min ) ? true : false )) {
                    table[ lin, col ] = new Tuple<double, double>( element.Item2, 0 );

                    linePositions[ lin ] = "x" + ( ( table.GetLength( 1 ) - 1 ) + lin );
                    col++;
                }
                lin++;
                col = 0;
            }
            Print( );
        }

        /// <summary>
        /// Fase um do processo de resolução. Verifica as linhas da coluna zero 
        /// procurando um elemento negativo, se o mesmo existe procura na primeira
        /// linha se existe um negativo
        /// </summary>
        /// <returns>Chama a fase dois</returns>
        public ObjectiveFunction FaseOne() {
            restartFaseOne:
            var allPositive = true;
            for (int lin = 1; lin < table.GetLength( 0 ); lin++) {
                if (table[ lin, 0 ].Item1 < 0) {
                    for (int col = 1; col < table.GetLength( 1 ); col++) {
                        if (table[ lin, col ].Item1 < 0) {
                            var quo = GetInvertedLessQuociente( lin, col );
                            table[ PermitedLine, PermitedColumn ] = new Tuple<double, double>( table[ PermitedLine, PermitedColumn ].Item1, quo );
                            table = AlgortimoDatroca( quo );
                            allPositive = false;
                            Print( );
                            goto restartFaseOne;
                        }
                    }
                }
            }

            if (!allPositive) {
                // SOLUÇÃO IMPOSSIVEL
                return GenerateSolution( ObjectiveFunction.RespType.Impossible );
            }

            // Fim do loop de algortimos da troca ou membro livre negativo não existe
            return FaseTwo( );
        }

        /// <summary>
        /// Fase dois do processo de resolucao. Verifica as linhas da coluna zero
        /// procurando um elemento positivo, se o mesmo existe procura na primeira 
        /// linha se existe um positivo.
        /// </summary>
        /// <returns>Retorna a devida solucao</returns>
        public ObjectiveFunction FaseTwo() {
            restartFaseTwo:
            var fxNegative = true;
            for (int col = 1; col < table.GetLength( 1 ); col++) {
                if (table[ 0, col ].Item1 > 0) {
                    for (int lin = 1; lin < table.GetLength( 1 ); lin++) {
                        if (table[ lin, col ].Item1 > 0) {
                            var quo = GetInvertedLessQuociente( lin, col );
                            table[ PermitedLine, PermitedColumn ] = new Tuple<double, double>( table[ PermitedLine, PermitedColumn ].Item1, quo );
                            table = AlgortimoDatroca( quo );
                            Print( );
                            fxNegative = false;
                            goto restartFaseTwo;
                        }
                    }
                }
            }

            for (int col = 1; col < table.GetLength( 1 ); col++) {
                if (table[ 0, col ].Item1 == 0) {
                    // MULTIPLAS SOLUÇÕES
                    return GenerateSolution( ObjectiveFunction.RespType.Multiple );
                }
            }

            if (fxNegative == false) {
                // SOLUÇÃO ILIMITADA
                return GenerateSolution( ObjectiveFunction.RespType.Unlimited );

            }
            if (fxNegative) {
                // ENCONTRADO A SOLUÇÃO OTIMA
                return GenerateSolution( ObjectiveFunction.RespType.Optimum );

            }

            return null;
        }

        /// <summary>
        /// Funcao que encontra o menor quociente invertido. 
        /// </summary>
        /// <param name="i">Linha atual</param>
        /// <param name="j">Coluna atual</param>
        /// <returns>Retorna o elemento permissivo invertido</returns>
        public double GetInvertedLessQuociente( int i, int j ) {
            double resp = 0, quo = 0, menor = Int32.MaxValue;
            for (int lin = 1; lin < table.GetLength( 0 ); lin++) {
                if (table[ lin, j ].Item1 != 0) {
                    quo = table[ lin, 0 ].Item1 / table[ lin, j ].Item1;
                    if (quo > 0 && quo < menor) {
                        resp = table[ lin, j ].Item1;
                        menor = quo;
                        i = lin;
                    }
                }
            }
            PermitedColumn = j;
            PermitedLine = i;
            return ( 1 / resp );
        }

        /// <summary>
        /// Realiza o processo do algoritmo da troca.
        /// </summary>
        /// <param name="quociente"></param>
        /// <returns>Nova tabela</returns>
        public Tuple<double, double>[ , ] AlgortimoDatroca( double quociente ) {
            // Multiplica os elementos da linha permissiva pelo quociente e insere na celula inferior da posicao
            for (int col = 0; col < table.GetLength( 1 ); col++) {
                if (col != PermitedColumn) {
                    table[ PermitedLine, col ] = new Tuple<double, double>( table[ PermitedLine, col ].Item1, table[ PermitedLine, col ].Item1 * quociente );
                }
            }

            // Multiplica os elementos da coluna permissica pelo quociente (multiplicado por menos um) e insere na celula inferior da posicao
            for (int lin = 0; lin < table.GetLength( 0 ); lin++) {
                if (lin != PermitedLine) {
                    table[ lin, PermitedColumn ] = new Tuple<double, double>( table[ lin, PermitedColumn ].Item1, table[ lin, PermitedColumn ].Item1 * -quociente );
                }
            }

            // Multiplica para os outros elementos da linha permissica pelos da coluna permissiva e insere na celula inferior da posicao
            for (int lin = 0; lin < table.GetLength( 0 ); lin++) {
                for (int col = 0; col < table.GetLength( 1 ); col++) {
                    if (lin != PermitedLine && col != PermitedColumn) {
                        table[ lin, col ] = new Tuple<double, double>( table[ lin, col ].Item1, table[ PermitedLine, col ].Item1 * table[ lin, PermitedColumn ].Item2 );
                    }
                }
            }

            // Realiza a troca das variaveis da linha permissiva pelo o da coluna permissiva
            var temp = columnPositions[ PermitedColumn ];
            columnPositions[ PermitedColumn ] = linePositions[ PermitedLine ];
            linePositions[ PermitedLine ] = temp;

            // Gera a nova tabela, trocando as celulas superiores pelas infeirores para a linha e coluna permissiva e soma as 
            // celulas inferiores e superiores para as outra posicoes
            Tuple<double, double>[ , ] resp = new Tuple<double, double>[ table.GetLength( 0 ), table.GetLength( 1 ) ];
            for (int i = 0; i < table.GetLength( 0 ); i++) {
                for (int o = 0; o < table.GetLength( 1 ); o++) {
                    if (i == PermitedLine || o == PermitedColumn) {
                        resp[ i, o ] = new Tuple<double, double>( table[ i, o ].Item2, 0 );
                    } else {
                        resp[ i, o ] = new Tuple<double, double>( table[ i, o ].Item1 + table[ i, o ].Item2, 0 );
                    }
                }
            }
            return resp;
        }

        // Formata a resposta para cada tipo de solucao possivel
        public ObjectiveFunction GenerateSolution( ObjectiveFunction.RespType solutionType ) {
            var resp = new ObjectiveFunction( objectiveFunction.Type ) {
                Solution = solutionType
            };
            var value = table[ 0, 0 ].Item1;
            if (objectiveFunction.Type == ObjectiveFunction.FuncType.Max) {
                value = value * -1;
            }
            resp.Add( "Z", value );
            for (int i = 1; i < linePositions.GetLength( 0 ); i++) {
                resp.Add( linePositions[ i ], table[ i, 0 ].Item1 );
            }
            for (int i = 1; i < columnPositions.GetLength( 0 ); i++) {
                resp.Add( columnPositions[ i ], 0 );
            }
            return resp;
        }

        /// <summary>
        /// Mostra na tela a tabela atual. 
        /// Somente no debug.
        /// </summary>
        public void Print() {
            Debug.WriteLine( "----------------------------------------------------------------------" );
            for (int lin = 0; lin < table.GetLength( 0 ); lin++) {
                for (int col = 0; col < table.GetLength( 1 ); col++) {
                    Debug.Write( table[ lin, col ].Item1 + " / " + table[ lin, col ].Item2 + "\t|\t" );
                }
                Debug.WriteLine( "" );
            }
            Debug.WriteLine( "----------------------------------------------------------------------" );
        }


    }
}

