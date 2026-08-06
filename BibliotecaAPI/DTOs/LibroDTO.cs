using BibliotecaAPI.Entidades;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public int AutorId { get; set; }
        public List<AutorLibro>? AutoresLibros { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public required string Genero { get; set; }
        public required string ISBN { get; set; }
        public int NumeroPaginas { get; set; }
    }
}
