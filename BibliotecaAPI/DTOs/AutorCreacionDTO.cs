using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public required string Nombre { get; set; }
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public required string Apellido { get; set; }
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public required string Nacionalidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [StringLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public required string Biografia { get; set; }
    }
}
