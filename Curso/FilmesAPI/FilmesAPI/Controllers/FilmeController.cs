using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        //private static List<Filme> filmes = new List<Filme>();
        //private static int id = 1;
        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }
       
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            //filme.Id = id++;
            //filmes.Add(filme);
            //Console.WriteLine(filme.Titulo);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { Id = filme.Id }, filme);
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes()
        {
            //return Ok(filmes);
            return _context.Filmes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            /* foreach(Filme filme in filmes)
             {
                 if(filme.Id == id)
                 {
                     return filme;
                 }
             }*/

            //Filme filme = filmes.FirstOrDefault(filme => filme.Id == id);
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if(filme != null)
            {
                return Ok(filme);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] Filme filmeNovo)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            filme.Titulo = filmeNovo.Titulo;
            filme.Genero = filmeNovo.Genero;
            filme.Duracao = filmeNovo.Duracao;
            filme.Diretor = filmeNovo.Diretor;

            _context.SaveChanges();

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
