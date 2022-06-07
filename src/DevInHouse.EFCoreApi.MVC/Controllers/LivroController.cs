using DevInHouse.EFCoreApi.Application.ApplicationServices;
using DevInHouse.EFCoreApi.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevInHouse.EFCoreApi.MVC.Controllers
{
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
            var livroCreateViewModel = await _livroApplicationService.InicializarLivroCreateViewModelAsync();
            
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LivroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
