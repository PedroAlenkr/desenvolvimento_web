using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Models
{
    public class Conexao : DbContext
    {
        public Conexao(DbContextOptions<Conexao> options) : base(options){}

        public DbSet<UserModel> Users {get; set;}

        public DbSet<mvc.Models.SessaoModel>? SessaoModel { get; set; }

        public DbSet<mvc.Models.ComprasModel>? ComprasModel { get; set; }
    }
    //<input asp-for="tipo" class="btn" value="Admin"/> esteregg
}