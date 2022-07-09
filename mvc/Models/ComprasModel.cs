using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    [Table("db_compras")]
    public class ComprasModel
    {
        [Column("protocolo")]
        public int Id { get; set; }
        [Column("evento")]
        public String evento { get; set; }
        [Column("qtd_ingressos")]
        public int ingressos { get; set; }
        [Column("dt_compra")]
        public String dt_compra { get; set; }
        [Column("dt_evento")]
        public String dt_evento {get;set;}
        [Column("cpf")]
        public long? cpf {get;set;}
        [Column("pagamento")]
        public String pagamento {get;set;}
        [Column("parcelas")]
        public int parcelas { get; set; }
        [Column("preco_total")]
        public double preco_total {get;set;}
        [Column("id_sessao")]
        public int id_sessao {get;set;}
    }
}