using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    [Table("db_usuarios")]
    public class UserModel
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("nome")]
        public String nome { get; set; }
        [Column("email")]
        public String email { get; set; }
        [Column("Senha")]
        public String senha { get; set; }
        [Column("nascimento")]
        public String? nascimento {get;set;}
        [Column("cpf")]
        public long? cpf {get;set;}
        [Column("sexo")]
        public Char? sexo {get;set;}
        [Column("Tipo")]
        public String tipo { get; set; }
    }
}