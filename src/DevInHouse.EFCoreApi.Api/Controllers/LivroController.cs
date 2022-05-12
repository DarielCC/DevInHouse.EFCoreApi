using DevInHouse.EFCoreApi.Api.DTOs;
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
        public async Task<IActionResult> ObterLivrosAsync(string? titulo)
        {
            IEnumerable<Livro> livros = await _livroService.ObterLivrosAsync(titulo);

            IEnumerable<LivroDTO> livrosDTO = livros.Select(livro => new LivroDTO()
            {
                Titulo = livro.Titulo,
                Preco = livro.Preco,
                DataPublicacao = livro.DataPublicacao,
                Categoria = new CategoriaDTO()
                {
                    Nome = livro.Categoria.Nome
                },
                Autor = new AutorDTO()
                {
                    Nome = livro.Autor.Nome
                }
            });

            return Ok(livrosDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterLivroPorIdAsync(int id)
        {
            Livro? livro = await _livroService.ObterPorIdAsync(id);

            if (livro == null)
            {
                return NotFound();
            }

            LivroDTO? livroDTO = new LivroDTO()
            {
                Titulo = livro.Titulo,
                Preco = livro.Preco,
                DataPublicacao = livro.DataPublicacao,
                Categoria = new CategoriaDTO()
                {
                    Nome = livro.Categoria.Nome
                },
                Autor = new AutorDTO()
                {
                    Nome = livro.Autor.Nome
                }
            };

            return Ok(livroDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CriarLivroAsync(LivroRequest livro)
        {
            try
            {
                int id = await _livroService.CriarLivroAsync(livro.Titulo, livro.CategoriaId, livro.AutorId, livro.DataPublicacao, livro.Preco);
                return CreatedAtAction(nameof(CriarLivroAsync), new { Id = id }, id);
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
        public async Task<IActionResult> AtualizarLivroAsync(int id, LivroRequest livroRequest)
        {
            try
            {
                await _livroService.AtualizarLivroAsync(id, livroRequest.Titulo, livroRequest.CategoriaId, livroRequest.AutorId, livroRequest.DataPublicacao, livroRequest.Preco);
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
        public async Task<IActionResult> ExcluirLivroAsync(int id)
        {
            try
            {
                await _livroService.RemoverLivroAsync(id);
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