using DevInHouse.EFCoreApi.Api.DTOs;
using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInHouse.EFCoreApi.Api.Controllers.v2
{
    [ApiController]
    [ApiVersion("2")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    [ApiExplorerSettings(GroupName = "autoresv2")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService) => _autorService = autorService;

        [HttpGet]
        public async Task<IActionResult> ObterAutoresAsync([FromQuery] string nome)
        {
            IEnumerable<Autor> autores = await _autorService.ObterAutoresV2Async(nome);

            IEnumerable<AutorDTO> autoresDTO = autores.Select(autor => new AutorDTO()
            {
                Nome = autor.Nome
            });

            return Ok(autoresDTO);
        }
    }
}
