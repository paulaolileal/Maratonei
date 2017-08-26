using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Maratonei.Models {
    [Table("tb_serie")]
    public class Serie {
        [Key,Column("serie_id")]
        public int SerieId { get; set; }
        [Column("nome")]
        public int Nome { get; set; }
        [Column("tempo_de_duracao")]
        public string TempoDeDuracao { get; set; }
    }
}
