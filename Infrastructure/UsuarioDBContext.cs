using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UsuarioDBContext : DbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }  

        public UsuarioDBContext(DbContextOptions options) : base(options) { }
    }
}
