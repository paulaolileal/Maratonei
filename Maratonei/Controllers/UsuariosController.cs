using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Maratonei.Models;

namespace Maratonei.Controllers {
    [Produces( "application/json" )]
    [Route( "api/Usuarios" )]
    public class UsuariosController : Controller {
        private readonly EntidadesContexto _context;

        public UsuariosController( EntidadesContexto context ) {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public IEnumerable<Usuario> GetUsuarios() {
            return _context.Usuarios;
        }

        // GET: api/Usuarios/5
        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetUsuario( [FromRoute] int id ) {
            if (!ModelState.IsValid) {
                return BadRequest( ModelState );
            }

            var usuario = await _context.Usuarios.SingleOrDefaultAsync( m => m.UsuarioId == id );

            if (usuario == null) {
                return NotFound( );
            }

            return Ok( usuario );
        }

        // PUT: api/Usuarios/5
        [HttpPut( "{id}" )]
        public async Task<IActionResult> PutUsuario( [FromRoute] int id, [FromBody] Usuario usuario ) {
            if (!ModelState.IsValid) {
                return BadRequest( ModelState );
            }

            if (id != usuario.UsuarioId) {
                return BadRequest( );
            }

            _context.Entry( usuario ).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync( );
            } catch (DbUpdateConcurrencyException) {
                if (!UsuarioExists( id )) {
                    return NotFound( );
                } else {
                    throw;
                }
            }

            return NoContent( );
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<IActionResult> PostUsuario( [FromBody] Usuario usuario ) {
            if (!ModelState.IsValid) {
                return BadRequest( ModelState );
            }

            _context.Usuarios.Add( usuario );
            await _context.SaveChangesAsync( );

            return CreatedAtAction( "GetUsuario", new { id = usuario.UsuarioId }, usuario );
        }

        // DELETE: api/Usuarios/5
        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteUsuario( [FromRoute] int id ) {
            if (!ModelState.IsValid) {
                return BadRequest( ModelState );
            }

            var usuario = await _context.Usuarios.SingleOrDefaultAsync( m => m.UsuarioId == id );
            if (usuario == null) {
                return NotFound( );
            }

            _context.Usuarios.Remove( usuario );
            await _context.SaveChangesAsync( );

            return Ok( usuario );
        }

        private bool UsuarioExists( int id ) {
            return _context.Usuarios.Any( e => e.UsuarioId == id );
        }
    }
}