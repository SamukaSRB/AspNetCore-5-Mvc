using Microsoft.EntityFrameworkCore;

namespace FilmsJust.Models
{
    public class Context : DbContext
    {
        public Context(){ }
        public Context(DbContextOptions options): base (options){}       
        public  DbSet<Cliente> Clientes {get;set;}     
         public DbSet<Filme> Filmes { get; set; }
         public DbSet<Genero> Generos { get; set; } 
         public DbSet<Locacao> Locacoes { get; set;}  
    }
}

