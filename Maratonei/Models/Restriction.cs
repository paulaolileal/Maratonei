using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Models {
    public class Restriction {
        public List<Tuple<string, double>> R;
        public FuncType Type;
        public enum FuncType { GreaterEqual = 1, LessEqual = 0 }

        public Restriction( FuncType type ) {
            R = new List<Tuple<string, double>>( );
            Type = type;
        }

        public void Add( string variable, double value ) {
            R.Add( new Tuple<string, double>( variable, value ) );
        }

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
