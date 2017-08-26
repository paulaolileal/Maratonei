using System;
using System.Linq;

namespace Maratonei.Models {
    public static class InicializaBD {
        public static void Initialize( EntidadesContexto context ) {
            context.Database.EnsureCreated( );
            context.SaveChanges( );
        }
    }
}
