using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Nombre { get; set; }
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Apellido { get; set; }
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Nacionalidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [StringLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Biografia { get; set; }
        public List<AutorLibro> Libros { get; set; }
        // Constructor por defecto
        public Autor() { }
        // Constructor con parámetros
        public Autor(int id, string nombre, string apellido, string nacionalidad, DateTime fechaNacimiento, string biografia)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Nacionalidad = nacionalidad;
            FechaNacimiento = fechaNacimiento;
            Biografia = biografia;
        }
    }
}
