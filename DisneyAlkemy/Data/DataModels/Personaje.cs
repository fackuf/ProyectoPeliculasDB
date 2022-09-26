using System.ComponentModel.DataAnnotations;

namespace DisneyAlkemy.Data.DataModels
{
    public class Personaje : BaseEntity
    {
        [Required]
        public string imagenPersonaje { get; set; } = string.Empty;
        [Required]
        public string nombre { get; set; } = string.Empty;
        [Required]
        public int edad { get; set; }
        [Required]
        public float peso { get; set; }
        [Required]
        public string historia { get; set; } = string.Empty;
        [Required]
        public ICollection<Pelicula> peliculas { get; set; } = new List<Pelicula>();
    }
}
