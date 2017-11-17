using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Models {

    /// <summary>
    /// Objeto para restrição
    /// </summary>
    public class GLPKRestriction {
        public List<double> Values { get; set; }
        public double Disponibility { get; set; }
        public Operator Operation { get; set; }

        public enum Operator { LessOrEqual = 0, GreaterOrEqual = 1 };

        public GLPKRestriction(List<double> val) {
            Values = val;
        }

        public GLPKRestriction() {
            Values = new List<double>( );
        }
    }

    /// <summary>
    /// Objeto para função objetiva
    /// </summary>
    public class GLPKObjective {
        public List<double> Values { get; set; }
        public Operator Operation { get; set; }

        public enum Operator { Minimize = 0, Maximize = 1}

        public GLPKObjective(List<double> obj) {
            Values = obj;
        }

        public GLPKObjective() {
            Values = new List<double>( );
        }
    }

    /// <summary>
    /// Objeto para o modelo que une a lista de restrições e a função objetiva
    /// </summary>
    public class GLPKInput {
        public List<string> Variables { get; set; }
        public List<GLPKRestriction> Restrictions { get; set; }
        public GLPKObjective Objective { get; set; }

        public GLPKInput( List<string> var, List<GLPKRestriction> rest, GLPKObjective obj ) {
            Variables = var;
            Restrictions = rest;
            Objective = obj;
        }

        public GLPKInput() {
            Variables = new List<string>( );
            Restrictions = new List<GLPKRestriction>( );
            Objective = new GLPKObjective();
        }
    }
}
