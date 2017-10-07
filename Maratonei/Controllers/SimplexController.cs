using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Maratonei.Models;
using Maratonei.Controllers.Components.Simplex;
using Newtonsoft.Json;

namespace Maratonei.Controllers {
    [Produces( "application/json" )]
    [Route( "api/Simplex" )]
    public class SimplexController : Controller {
        private readonly EntidadesContexto _context;

        /// <summary>
        /// Construtor padrao
        /// </summary>
        /// <param name="context"></param>
        public SimplexController( EntidadesContexto context ) {
            _context = context;
        }

        /// <summary>
        /// Requisicao para resolucao do simplex
        /// </summary>
        /// <param name="SimplexInput">Funcao objetiva + lista de restricoes</param>
        /// <returns>Codigo da solução</returns>
        [HttpPost( "Solver/" )]
        [Route( "Simplex/Solver" )]
        public IActionResult Solver( [FromBody] SimplexInput SimplexInput ) {
            try {
                Simplex simplex = new Simplex( SimplexInput.ObjectiveFunction, SimplexInput.Restrictions );
                var resp = simplex.Solver( );
                return Ok( resp );
            } catch {
                return BadRequest( "It was not possible to calculate the simplex" );
            }
        }
    }
}