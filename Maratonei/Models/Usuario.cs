using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Models {
    [Table( "tb_usuario" )]
    public class Usuario {
        [Key, Column("usuario_id")]
        public int UsuarioId { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("senha")]
        public string Senha { get; set; }
    }
}
