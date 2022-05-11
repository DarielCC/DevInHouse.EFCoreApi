using DevInHouse.EFCoreApi.Api.DTOs.Request;
using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInHouse.EFCoreApi.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public ActionResult<List<Livro>> ObterLivros(string? titulo)
        {
            IEnumerable<Livro>? livros = _livroService.ObterLivros(titulo);
            if (livros == null || livros.Any())
            {
                return NoContent();
            }

            return Ok(livros);
        }

        [HttpGet("{id}")]
        public ActionResult<Livro> ObterLivroPorId(int id)
        {
            Livro? livro = _livroService.ObterPorId(id);
            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }

        [HttpPost]
        public ActionResult CriarLivro(LivroRequest livro)
        {
            try
            {
                int id = _livroService.CriarLivro(livro.Titulo, livro.CategoriaId, livro.AutorId, livro.DataPublicacao, livro.Preco);
                return CreatedAtAction(nameof(ObterLivroPorId), new { Id = id }, id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("{id}")]
        public ActionResult AtualizarLivro(int id, LivroRequest livroRequest)
        {
            try
            {
                _livroService.AtualizarLivro(id, livroRequest.Titulo, livroRequest.CategoriaId, livroRequest.AutorId, livroRequest.DataPublicacao, livroRequest.Preco);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult ExcluirLivro(int id)
        {
            try
            {
                _livroService.RemoverLivro(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                if (ex.ParamName.Equals("id"))
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }
    }
}