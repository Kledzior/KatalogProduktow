using Kledzik.KatalogSpiworow.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kledzik.KatalogSpiworow.Web.Controllers
{
    public class ProducenciController : Controller
    {
        private readonly IDataRepository _repo;

        public ProducenciController(IDataRepository repo)
        {
            _repo = repo;
        }

        // 1. LISTA
        public IActionResult Index()
        {
            var producenci = _repo.PobierzProducentow();
            return View(producenci);
        }

        // 2. DODAWANIE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // 2. DODAWANIE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string nazwa)
        {
            if (ModelState.IsValid)
            {
                var nowyProducent = _repo.UtworzNowegoProducenta();
                nowyProducent.Nazwa = nazwa;

                _repo.DodajProducenta(nowyProducent);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // 3. EDYCJA (GET)
        public IActionResult Edit(int id)
        {
            var producent = _repo.PobierzProducentow().FirstOrDefault(p => p.Id == id);
            if (producent == null) return NotFound();
            return View(producent);
        }

        // 3. EDYCJA (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string nazwa)
        {
            var producent = _repo.PobierzProducentow().FirstOrDefault(p => p.Id == id);
            if (producent == null) return NotFound();

            if (ModelState.IsValid)
            {
                producent.Nazwa = nazwa;
                _repo.EdytujProducenta(producent);
                return RedirectToAction(nameof(Index));
            }
            return View(producent);
        }

        // 4. USUWANIE (GET)
        public IActionResult Delete(int id)
        {
            var producent = _repo.PobierzProducentow().FirstOrDefault(p => p.Id == id);
            if (producent == null) return NotFound();
            return View(producent);
        }

        // 4. USUWANIE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var producent = _repo.PobierzProducentow().FirstOrDefault(p => p.Id == id);
            if (producent != null)
            {
                _repo.UsunProducenta(producent.Id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}