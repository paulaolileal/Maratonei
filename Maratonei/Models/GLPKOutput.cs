using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Models {
    public class GLPKOutput {

        public IDictionary<string, double> Variables { get; set; }
        public IDictionary<string, double> Objectives { get; set; }
        public OPTANO.Modeling.Optimization.Solver.SolutionStatus Status { get; set; }

        public GLPKOutput() {
            Variables = new Dictionary<string, double>( );
            Objectives = new Dictionary<string, double>( );
        }

        public GLPKOutput( IDictionary<string, double> var, IDictionary<string, double> obj) {
            Variables = var;
            Objectives = obj;
        }
    }
}
