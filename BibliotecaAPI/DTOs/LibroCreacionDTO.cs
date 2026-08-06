using BibliotecaAPI.Entidades;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class LibroCreacionDTO
    {
        [Required]
        [StringLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public required string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? Genero { get; set; }
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? ISBN { get; set; }
        public int NumeroPaginas { get; set; }
        public List<int> AutoresIds { get; set; }
    }
}
