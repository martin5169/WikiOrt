using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WikiORT.Models;

namespace WikiORT.Context
{

    public class WikiDatabaseContext : DbContext
    {
        public
        WikiDatabaseContext(DbContextOptions<WikiDatabaseContext> options)
        : base(options)
        {
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
       // public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<PalabraClave> PalabrasClaves { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        

        



    }
}
    
    

