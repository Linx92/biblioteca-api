using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public required string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Genero { get; set; }
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string ISBN { get; set; }
        public int NumeroPaginas { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<AutorLibro> Autores { get; set; }
        // Constructor por defecto
        public Libro() { }
        // Constructor con parámetros
        public Libro(int id, string titulo,DateTime fechaPublicacion, string genero, string isbn, int numeroPaginas)
        {
            Id = id;
            Titulo = titulo;
            FechaPublicacion = fechaPublicacion;
            Genero = genero;
            ISBN = isbn;
            NumeroPaginas = numeroPaginas;
        }
    }
}
