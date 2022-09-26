using DisneyAlkemy.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace DisneyAlkemy.DataAccess
{
    public class DisneyContext : DbContext
    {
        public DisneyContext(DbContextOptions<DisneyContext> options) : base(options)
        {

        }

        // Agregamos las tablas
        public DbSet<User>? users { get; set; }
        public DbSet<Genero>? generos { get; set; }
        public DbSet<Pelicula>? peliculas { get; set; }
        public DbSet<Personaje>? personajes { get; set; }
    }
}
