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

        public SimplexController( EntidadesContexto context ) {
            _context = context;
        }

        // GET: api/Series/5
        [HttpPost( "Solver/" )]
        [Route( "Simplex/Solver" )]
        public IActionResult Solver( [FromBody] SimplexInput SimplexInput ) {
            Simplex simplex = new Simplex( SimplexInput.ObjectiveFunction, SimplexInput.Restrictions );
            var resp = simplex.Solver( );

            return Ok( resp );
        }
    }
}