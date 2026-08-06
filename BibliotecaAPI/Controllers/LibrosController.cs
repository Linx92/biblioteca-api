using AutoMapper;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<Libro>> Get()
        {
            return await context.Libros.ToListAsync();
        }
        [HttpGet("{id:int}", Name ="ObtenerLibro")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await context.Libros
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (libro == null)
            {
                return NotFound();
            }
            return libro;
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {

            if (libroCreacionDTO.AutoresIds is null || libroCreacionDTO.AutoresIds.Count == 0)
            {
                ModelState.AddModelError(nameof(libroCreacionDTO.AutoresIds), "No se puede crear libros sin autores.");
                return ValidationProblem();
            }
            var autoresIdsExistentes = await context.Autores
                .Where(x => libroCreacionDTO.AutoresIds.Contains(x.Id))
                .Select(x => x.Id).ToListAsync();

            if (autoresIdsExistentes.Count != libroCreacionDTO.AutoresIds.Count)
            {
                var autoresNoExistentes = libroCreacionDTO.AutoresIds.Except(autoresIdsExistentes);
                var autoresNoExistentesString = string.Join(",", autoresNoExistentes);
                var mensajeError = $"Los siguientes autores no existen: {autoresNoExistentesString}";
                ModelState.AddModelError(nameof(libroCreacionDTO.AutoresIds), mensajeError);
                return ValidationProblem();
            }

            var libro = mapper.Map<Libro>(libroCreacionDTO);
            AsignarOrdenAutores(libro);
            context.Add(libro);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerLibro", new { id = libro.Id }, libro);
        }

        private void AsignarOrdenAutores(Libro libro)
        {
            if (libro.Autores == null) { return; }
            for (int i = 0; i < libro.Autores.Count; i++)
            {
                libro.Autores[i].Orden = i + 1;
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO)
        {
            if (libroCreacionDTO.AutoresIds is null || libroCreacionDTO.AutoresIds.Count == 0)
            {
                ModelState.AddModelError(nameof(libroCreacionDTO.AutoresIds), "No se puede crear libros sin autores.");
                return ValidationProblem();
            }
            var autoresIdsExistentes = await context.Autores
                .Where(x => libroCreacionDTO.AutoresIds.Contains(x.Id))
                .Select(x => x.Id).ToListAsync();

            if (autoresIdsExistentes.Count != libroCreacionDTO.AutoresIds.Count)
            {
                var autoresNoExistentes = libroCreacionDTO.AutoresIds.Except(autoresIdsExistentes);
                var autoresNoExistentesString = string.Join(",", autoresNoExistentes);
                var mensajeError = $"Los siguientes autores no existen: {autoresNoExistentesString}";
                ModelState.AddModelError(nameof(libroCreacionDTO.AutoresIds), mensajeError);
                return ValidationProblem();
            }
            var libroBD = await context.Libros
                .Include(x => x.Autores)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(libroBD is null)
            {
                return NotFound();
            }
            libroBD = mapper.Map(libroCreacionDTO, libroBD);

            AsignarOrdenAutores(libroBD);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Libros.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (registrosBorrados == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
