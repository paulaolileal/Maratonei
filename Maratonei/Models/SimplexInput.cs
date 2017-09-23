using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maratonei.Controllers.Components.Simplex;

namespace Maratonei.Models {
    public class SimplexInput {
        public ObjectiveFunction ObjectiveFunction { get; set; }
        public List<Restriction> Restrictions { get; set; }

        public SimplexInput( ObjectiveFunction objectiveFunction, List<Restriction> restrictions ) {
            ObjectiveFunction = objectiveFunction;
            Restrictions = restrictions;
        }

        public SimplexInput() {
            Restrictions = new List<Restriction>( );
        }
    }
}
