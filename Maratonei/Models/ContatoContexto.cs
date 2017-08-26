using Microsoft.EntityFrameworkCore;

namespace Maratonei.Models {
    public class ContatoContexto : DbContext {
        public ContatoContexto(DbContextOptions<ContatoContexto> options) : base( options ) {
        }
        public DbSet<Contato> Contatos { get; set; }
    }
}
