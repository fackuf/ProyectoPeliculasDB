using System.ComponentModel.DataAnnotations;

namespace DisneyAlkemy.Data.DataModels
{
    public class Genero: BaseEntity
    {
        [Required]
        public string imagen { get; set; } = string.Empty;
        [Required]
        public string nombre { get; set; } = string.Empty;
        [Required]
        public ICollection<Pelicula> peliculas { get; set; } = new List<Pelicula>();
        //ID Peliculas
    }
}
