using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Models {
    public class ObjectiveFunction {
        public List<Tuple<string, double>> Z;
        public FuncType Type;
        public RespType Solution;

        public enum FuncType { Max = 1, Min = 0 };
        public enum RespType { Optimum = 0, Unlimited = 1, Multiple = 2, Impossible = 3, NotASolution = 4 };

        public ObjectiveFunction() { }

        public ObjectiveFunction( FuncType funcType ) {
            Z = new List<Tuple<string, double>>( );
            Type = funcType;
            Solution = RespType.NotASolution;
        }

        public ObjectiveFunction( FuncType Type, RespType solution ) {
            Z = new List<Tuple<string, double>>( );
            Solution = solution;
        }

        public void Add( string variable, double value ) {
            Z.Add( new Tuple<string, double>( variable, value ) );
        }

        public List<Tuple<string, double>> Transform() {
            var TransformedZ = new List<Tuple<string, double>>( );
            if (Type == FuncType.Max) {
                Z.ForEach( element => TransformedZ.Add( new Tuple<string, double>( element.Item1, element.Item2 * -1 ) ) );
                return TransformedZ;
            }
            return Z;
        }
    }
}
