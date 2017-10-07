using Maratonei.Controllers.Components.Simplex;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Maratonei.Models {
    public static class InicializaBD {
        public static void Initialize( EntidadesContexto context ) {

            context.Database.EnsureCreated( );
            if (!context.Usuarios.Any( )) {
                context.Usuarios.Add( new Usuario { Nome = "teste", Senha = "teste", traktUser = "teste" } );
            }
            context.SaveChanges( );
        }
    }
}
