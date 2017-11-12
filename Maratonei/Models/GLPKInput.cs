using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Models {

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

    public class GLPKObjective {
        public List<double> Values { get; set; }

        public GLPKObjective(List<double> obj) {
            Values = obj;
        }

        public GLPKObjective() {
            Values = new List<double>( );
        }
    }

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
