using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Models {
    public class EntidadesContexto : DbContext {
        public DbSet<Usuario> Usuarios { get; set; }

        public EntidadesContexto( DbContextOptions<EntidadesContexto> options ) : base( options ) { }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
        }
    }
}
