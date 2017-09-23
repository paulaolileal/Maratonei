using Maratonei.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Controllers.Components.Simplex {
    public class Simplex {

        private Tuple<double, double>[ , ] table;
        private ObjectiveFunction objectiveFunction;
        private List<Restriction> restrictionsList;

        private string[ ] columnPositions;
        private string[ ] linePositions;

        private int PermitedColumn;
        private int PermitedLine;

        public Simplex( ObjectiveFunction po, List<Restriction> restrictions ) {
            objectiveFunction = po;
            restrictionsList = restrictions;

            table = new Tuple<double, double>[ restrictions.Count( ) + 1, po.Z.Count( ) + 1 ];

            columnPositions = new string[ po.Z.Count( ) + 1 ];
            linePositions = new string[ restrictions.Count( ) + 1 ];
        }

        public ObjectiveFunction Solver() {
            PopuleTable( );
            return FaseOne( );
        }

        public void PopuleTable() {
            int col = 1;
            table[ 0, 0 ] = new Tuple<double, double>( 0, 0 );
            columnPositions[ 0 ] = "ml";

            foreach (var element in objectiveFunction.Transform( )) {
                table[ 0, col ] = new Tuple<double, double>( element.Item2, 0 );
                columnPositions[ col ] = element.Item1;
                col++;
            }

            col = 0;
            int lin = 1;
            linePositions[ 0 ] = "f(x)";
            foreach (var restriction in restrictionsList) {
                foreach (var element in restriction.Transform( )) {
                    table[ lin, col ] = new Tuple<double, double>( element.Item2, 0 );

                    linePositions[ lin ] = "x" + ( ( table.GetLength( 1 ) - 1 ) + lin );
                    col++;
                }
                lin++;
                col = 0;
            }
            //FaseOne( );
        }

        public ObjectiveFunction FaseOne() {
            restartFaseOne:
            for (int lin = 1; lin < table.GetLength( 0 ); lin++) {
                if (table[ lin, 0 ].Item1 < 0) {
                    for (int col = 1; col < table.GetLength( 1 ); col++) {
                        if (table[ lin, col ].Item1 < 0) {
                            var quo = GetInvertedLessQuociente( lin, col );
                            table[ PermitedLine, PermitedColumn ] = new Tuple<double, double>( table[ PermitedLine, PermitedColumn ].Item1, quo );
                            table = AlgortimoDatroca( quo );
                            goto restartFaseOne;
                        }
                    }
                    // SOLUÇÃO IMPOSSIVEL
                    return GenerateSolution( ObjectiveFunction.RespType.Impossible );
                }
            }
            // Fim do loop de algortimos da troca ou membro livre negativo não existe
            return FaseTwo( );
        }

        public ObjectiveFunction FaseTwo() {
            restartFaseTwo:
            for (int col = 1; col < table.GetLength( 0 ); col++) {
                if (table[ 0, col ].Item1 > 0) {
                    for (int lin = 1; lin < table.GetLength( 1 ); lin++) {
                        if (table[ lin, col ].Item1 > 0) {
                            var quo = GetInvertedLessQuociente( lin, col );
                            table[ PermitedLine, PermitedColumn ] = new Tuple<double, double>( table[ PermitedLine, PermitedColumn ].Item1, quo );
                            table = AlgortimoDatroca( quo );
                            goto restartFaseTwo;
                        }
                    }
                    // SOLUÇÃO ILIMITADA
                    Debug.WriteLine( "" );
                    return GenerateSolution( ObjectiveFunction.RespType.Unlimited );

                } else if (table[ 0, col ].Item1 < 0) {
                    // ENCONTRADO A SOLUÇÃO OTIMA
                    Debug.WriteLine( "" );
                    return GenerateSolution( ObjectiveFunction.RespType.Optimum );

                } else {
                    // MULTIPLAS SOLUÇÕES
                    return GenerateSolution( ObjectiveFunction.RespType.Multiple );
                }
            }
            return null;
        }

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

        public Tuple<double, double>[ , ] AlgortimoDatroca( double quo ) {
            for (int col = 0; col < table.GetLength( 1 ); col++) {
                if (col != PermitedColumn) {
                    table[ PermitedLine, col ] = new Tuple<double, double>( table[ PermitedLine, col ].Item1, table[ PermitedLine, col ].Item1 * quo );
                }
            }
            for (int lin = 0; lin < table.GetLength( 0 ); lin++) {
                if (lin != PermitedLine) {
                    table[ lin, PermitedColumn ] = new Tuple<double, double>( table[ lin, PermitedColumn ].Item1, table[ lin, PermitedColumn ].Item1 * -quo );
                }
            }
            for (int lin = 0; lin < table.GetLength( 0 ); lin++) {
                for (int col = 0; col < table.GetLength( 1 ); col++) {
                    if (lin != PermitedLine && col != PermitedColumn) {
                        table[ lin, col ] = new Tuple<double, double>( table[ lin, col ].Item1, table[ PermitedLine, col ].Item1 * table[ lin, PermitedColumn ].Item2 );
                    }
                }
            }

            var temp = columnPositions[ PermitedColumn ];
            columnPositions[ PermitedColumn ] = linePositions[ PermitedLine ];
            linePositions[ PermitedLine ] = temp;

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

        public ObjectiveFunction GenerateSolution( ObjectiveFunction.RespType solutionType ) {
            var resp = new ObjectiveFunction( objectiveFunction.Type ) {
                Solution = solutionType
            };
            resp.Add( "Z", table[ 0, 0 ].Item1 );
            for (int i = 1; i < linePositions.GetLength( 0 ); i++) {
                resp.Add( linePositions[ i ], table[ i, 0 ].Item1 );
            }
            for (int i = 1; i < columnPositions.GetLength( 0 ); i++) {
                resp.Add( columnPositions[ i ], 0 );
            }
            return resp;
        }

        public void Print() {
            Debug.WriteLine( "+++++++++++++++++++++++++++++++" );
            for (int lin = 0; lin < table.GetLength( 0 ); lin++) {
                for (int col = 0; col < table.GetLength( 1 ); col++) {
                    Debug.Write( table[ lin, col ].Item1 + " / " + table[ lin, col ].Item2 + "    " );
                }
                Debug.WriteLine( "" );
            }
            Debug.WriteLine( "+++++++++++++++++++++++++++++++" );
        }


    }
}

