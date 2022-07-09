using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    [Table("db_logs")]
    public class LogModel
    {
        [Column("isLogged?")]
        public int Id { get; set; }
    }
}