using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maratonei.Controllers.Components.Simplex;

namespace Maratonei.Models {
    /// <summary>
    /// Modelo de entrada para requisicoes
    /// </summary>
    public class SimplexInput {
        // Funcao objetiva
        public ObjectiveFunction ObjectiveFunction { get; set; }
        // Lista de restricoes
        public List<Restriction> Restrictions { get; set; }

        /// <summary>
        /// Contrutor que recebe uma funcao objetiva e a lista de restricoes
        /// </summary>
        /// <param name="objectiveFunction"></param>
        /// <param name="restrictions"></param>
        public SimplexInput( ObjectiveFunction objectiveFunction, List<Restriction> restrictions ) {
            ObjectiveFunction = objectiveFunction;
            Restrictions = restrictions;
        }

        /// <summary>
        /// Construtor padrao
        /// </summary>
        public SimplexInput() {
            Restrictions = new List<Restriction>( );
        }
    }
}
