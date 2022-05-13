using DevInHouse.EFCoreApi.Api.DTOs;
using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevInHouse.EFCoreApi.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [ApiExplorerSettings(GroupName = "autores")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService) => _autorService = autorService;

        [HttpGet]
        public async Task<IActionResult> ObterAutoresAsync()
        {
            IEnumerable<Autor> autores = await _autorService.ObterAutoresAsync();

            IEnumerable<AutorDTO> autoresDTO = autores.Select(autor => new AutorDTO()
            {
                Nome = autor.Nome
            });

            return Ok(autoresDTO);
        }
    }
}
