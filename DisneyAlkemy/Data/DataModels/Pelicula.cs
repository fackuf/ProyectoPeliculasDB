using System.ComponentModel.DataAnnotations;

namespace DisneyAlkemy.Data.DataModels
{
    public enum calificacion
    {
        Uno = 1,
        Dos = 2,
        Tres = 3,
        Cuatro = 4,
        Cinco = 5

    }
    public class Pelicula : BaseEntity
    {
        [Required]
        public string imagenPelicula { get; set; } = string.Empty;
        [Required]
        public string titulo { get; set; } = string.Empty;
        [Required]
        public DateTime? FechaCreacion { get; set; }
        public calificacion calificacion { get; set; } = calificacion.Cinco;
        [Required]
        public ICollection<Personaje> personajes { get; set; } = new List<Personaje>();
        [Required]
        public ICollection<Genero> generos { get; set; } = new List<Genero>();
    }
}
