using DevInHouse.EFCoreApi.Application.ApplicationServices;
using DevInHouse.EFCoreApi.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevInHouse.EFCoreApi.MVC.Controllers
{
    [Authorize]
    public class LivroController : Controller
    {
        private readonly ILivroApplicationService _livroApplicationService;

        public LivroController(ILivroApplicationService livroApplicationService)
            => _livroApplicationService = livroApplicationService;

        // GET: LivroController
        public async Task<IActionResult> Index(string titulo)
            => View(await _livroApplicationService.ObterLivrosAsync(titulo));

        // GET: LivroController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LivroController/Create
        public async Task<IActionResult> Create()
        {
            LivroCreateViewModel? livroCreateViewModel = await _livroApplicationService.InicializarLivroCreateViewModelAsync();

            return View(livroCreateViewModel);
        }

        // POST: LivroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivroCreateViewModel livroViewModel)
        {
            try
            {
                await _livroApplicationService.CriarLivroAsync(livroViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: LivroController/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            LivroEditViewModel? livroCreateViewModel = await _livroApplicationService.InicializarLivroEditViewModelAsync(id);

            return View(livroCreateViewModel);
        }

        // POST: LivroController/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues titulo = collection["Titulo"];
                Microsoft.Extensions.Primitives.StringValues autorId = collection["AutorId"];
                Microsoft.Extensions.Primitives.StringValues categoriaId = collection["CategoriaId"];
                Microsoft.Extensions.Primitives.StringValues publicacao = collection["Publicacao"];
                Microsoft.Extensions.Primitives.StringValues preco = collection["Preco"];

                LivroEditViewModel? livroViewModel = new LivroEditViewModel()
                {
                    Id = id,
                    AutorId = Convert.ToInt32(autorId),
                    CategoriaId = Convert.ToInt32(categoriaId),
                    Preco = Convert.ToDecimal(preco),
                    Publicacao = Convert.ToDateTime(publicacao),
                    Titulo = titulo
                };

                await _livroApplicationService.EditarLivroAsync(livroViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivroController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LivroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
