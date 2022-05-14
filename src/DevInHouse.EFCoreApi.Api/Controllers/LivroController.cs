using DevInHouse.EFCoreApi.Api.DTOs;
using DevInHouse.EFCoreApi.Api.DTOs.Request;
using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Core.Interfaces;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevInHouse.EFCoreApi.Api.Controllers
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "livros")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;
        private readonly INotificacaoService _notificacaoService;

        public LivroController(ILivroService livroService, INotificacaoService notificacaoService)
        {
            _livroService = livroService;
            _notificacaoService = notificacaoService;
        }

        [HttpGet]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, description: "Livros", type: typeof(IEnumerable<LivroDTO>))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Erro no servidor")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [SwaggerOperation(Summary = "Retorna lista de livro", Tags = new[] { "Livro" })]
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
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, description: "Livro", type: typeof(LivroDTO))]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, description: "Livro não existe")]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Erro no servidor")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [SwaggerOperation(Summary = "Retorna livro por id", Tags = new[] { "Livro" })]
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
        [SwaggerResponse(statusCode: StatusCodes.Status201Created, description: "Livro criado com sucesso")]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, description: "Autor não existe")]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, description: "Livro inválido")]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Erro no servidor")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [SwaggerOperation(Summary = "Cria novo livro", Tags = new[] { "Livro" })]
        public async Task<IActionResult> CriarLivroAsync(LivroRequest livro)
        {
            int id = await _livroService.CriarLivroAsync(livro.Titulo, livro.CategoriaId, livro.AutorId, livro.DataPublicacao, livro.Preco);
            return Created($"{Request.Host}{Request.Path}/{id}", id);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(statusCode: StatusCodes.Status204NoContent, description: "Livro atualizado com sucesso")]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, description: "Livro não existe")]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Erro no servidor")]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, description: "Livro inválido")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [SwaggerOperation(Summary = "Atualiza livro", Tags = new[] { "Livro" })]
        public async Task<IActionResult> AtualizarLivroAsync(int id, LivroRequest livroRequest)
        {
            await _livroService.AtualizarLivroAsync(id, livroRequest.Titulo, livroRequest.CategoriaId, livroRequest.AutorId, livroRequest.DataPublicacao, livroRequest.Preco);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(statusCode: StatusCodes.Status204NoContent, description: "Livro apagado com sucesso")]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, description: "Livro não existe")]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Erro no servidor")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [SwaggerOperation(Summary = "Apaga livro", Tags = new[] { "Livro" })]
        public async Task<IActionResult> ExcluirLivroAsync(int id)
        {
            await _livroService.RemoverLivroAsync(id);
            return NoContent();
        }
    }
}