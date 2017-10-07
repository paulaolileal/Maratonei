using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Models {
    /// <summary>
    /// Objeto com a estrutura da restricao
    /// </summary>
    public class Restriction {
        public List<Tuple<string, double>> R;
        public FuncType Type;

        // Tipo da funcao: menor ou igual ou maior e igual
        public enum FuncType { LessEqual = 0, GreaterEqual = 1 }

        /// <summary>
        /// Construtor padrao
        /// </summary>
        public Restriction( FuncType type ) {
            R = new List<Tuple<string, double>>( );
            Type = type;
        }

        /// <summary>
        /// Adiciona um elemento a funcao objetiva
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        public void Add( string variable, double value ) {
            R.Add( new Tuple<string, double>( variable, value ) );
        }

        /// <summary>
        /// Foi abordado uma maneira diferente de transformar as funcoes, 
        /// multiplicando por -1 quando for de minimizacao para facilitar o trabalho
        /// </summary>
        /// <returns></returns>
        public List<Tuple<string, double>> Transform( bool isMin = false ) {
            var TransformedR = new List<Tuple<string, double>>( );

            if (isMin) {
                foreach (var element in R) {
                    TransformedR.Add( new Tuple<string, double>( element.Item1, element.Item2 * -1 ) );
                }
                return TransformedR;
            }else if (Type == FuncType.GreaterEqual) {
                foreach (var element in R) {
                    TransformedR.Add( new Tuple<string, double>( element.Item1, element.Item2 * -1 ) );
                }
                return TransformedR;
            }
            return R;
        }
    }
}
