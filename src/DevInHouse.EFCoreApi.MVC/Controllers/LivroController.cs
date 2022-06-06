using DevInHouse.EFCoreApi.Application.ApplicationServices;
using DevInHouse.EFCoreApi.Application.ViewModels;
using DevInHouse.EFCoreApi.MVC.Models;
using Microsoft.AspNetCore.Http;
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: LivroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
