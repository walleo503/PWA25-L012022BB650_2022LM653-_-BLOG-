using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace L01_2022BB650_2022LM653.Models
{
    public class DatosContext : DbContext
    {
        public DatosContext(DbContextOptions <DatosContext> options) : base (options)
        {
        
        }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Publicaciones> Publicaciones { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Calificaciones> Calificaciones { get;set; }


    }
}
