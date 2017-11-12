using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Solver.GLPK;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maratonei
{
    [Route("api/[controller]")]
    public class GLPKControllerx : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /// <summary>
        /// Requisicao para resolucao do simplex
        /// </summary>
        /// <param name="SimplexInput">Funcao objetiva + lista de restricoes</param>
        /// <returns>Codigo da solução</returns>
        [HttpPost("Solver/")]
        [Route("GLPK/Solver")]
        public IActionResult Solver()
        {
            //try {
            var model = new Model();
            var x = new Variable("x");
            var y = new Variable("y");
            model.AddConstraint(10 * x + 20 * y >= 2);
            model.AddConstraint(40 * x + 60 * y >= 64);
            model.AddConstraint(50 * x + 20 * y >= 34);
            model.AddObjective(new Objective(0.6 * x + 0.8 * y));

            var solver = new GLPKSolver();
            solver.Solve(model);
            var solution = solver.Solve(model);

            return Ok(solution.ObjectiveValues);
            //} catch {
            //    return BadRequest( "It was not possible to calculate the simplex" );
            //}
        }
    }
}
