using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Models {
    /// <summary>
    /// Objeto com a estrutura da funcao objetiva
    /// </summary>
    public class ObjectiveFunction {
        public List<Tuple<string, double>> Z;
        public FuncType Type;
        public RespType Solution;

        // Tipo da funcao: minimizacao ou maximizacao
        public enum FuncType { Min = 0, Max = 1  };
        // Possiveis tipos de solucao
        public enum RespType { Optimum = 0, Unlimited = 1, Multiple = 2, Impossible = 3, NotASolution = 4 };

        /// <summary>
        /// Construtor padrao
        /// </summary>
        public ObjectiveFunction() { }

        /// <summary>
        /// Construtor passando o tipo de funcao
        /// </summary>
        public ObjectiveFunction( FuncType funcType ) {
            Z = new List<Tuple<string, double>>( );
            Type = funcType;
            Solution = RespType.NotASolution;
        }

        /// <summary>
        /// Construtor passando o tipo de funcao e o tipo de solucao
        /// </summary>
        public ObjectiveFunction( FuncType Type, RespType solution ) {
            Z = new List<Tuple<string, double>>( );
            Solution = solution;
        }

        /// <summary>
        /// Adiciona um elemento a funcao objetiva
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        public void Add( string variable, double value ) {
            Z.Add( new Tuple<string, double>( variable, value ) );
        }

        /// <summary>
        /// Foi abordado uma maneira diferente de transformar as funcoes, 
        /// multiplicando por -1 quando for de minimizacao para facilitar o trabalho
        /// </summary>
        /// <returns></returns>
        public List<Tuple<string, double>> Transform() {
            var TransformedZ = new List<Tuple<string, double>>( );
            if (Type == FuncType.Min) {
                Z.ForEach( element => TransformedZ.Add( new Tuple<string, double>( element.Item1, element.Item2 * -1 ) ) );
                return TransformedZ;
            }
            return Z;
        }
    }
}
