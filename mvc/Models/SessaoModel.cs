using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    [Table("db_sessoes")]
    public class SessaoModel
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("evento")]
        public String evento { get; set; }
        [Column("local")]
        public String local { get; set; }
        [Column("data")]
        public String data { get; set; }
        [Column("inicio")]
        public String hr_inicio { get; set; }
        [Column("fim")]
        public String hr_fim { get; set; }
        [Column("preco")]
        public Double preco { get; set; }
        [Column("assentos")]
        public Int32 assentos { get; set; }
        [Column("comprados")]
        public Int32 comprados { get; set; }
    }
}