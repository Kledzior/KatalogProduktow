using Kledzik.KatalogSpiworow.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kledzik.KatalogSpiworow.Web.Controllers
{
    public class SpiworyController : Controller
    {
        private readonly IDataRepository _repo;

        public SpiworyController(IDataRepository repo)
        {
            _repo = repo;
        }

        // 1. LISTA
        public IActionResult Index()
        {
            var spiwory = _repo.PobierzSpiwory();
            return View(spiwory);
        }

        // 2. DODAWANIE (GET)
        public IActionResult Create()
        {
            ViewBag.Producenci = _repo.PobierzProducentow();
            return View();
        }

        // 2. DODAWANIE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POPRAWKA 1: Zmiana typu 'masa' z double na int
        public IActionResult Create(string model, int temperatura, int masa, decimal cena, int producentId)
        {
            if (ModelState.IsValid)
            {
                var nowySpiwor = _repo.UtworzNowySpiwor();

                nowySpiwor.Model = model;
                nowySpiwor.Temperatura = temperatura;
                nowySpiwor.Masa = masa;
                nowySpiwor.Cena = cena;
                nowySpiwor.ProducentId = producentId;

                _repo.DodajSpiwor(nowySpiwor);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Producenci = _repo.PobierzProducentow();
            return View();
        }

        // 3. EDYCJA (GET)
        public IActionResult Edit(int id)
        {
            var spiwor = _repo.PobierzSpiwory().FirstOrDefault(s => s.Id == id);

            if (spiwor == null)
            {
                return NotFound();
            }

            ViewBag.Producenci = _repo.PobierzProducentow();
            return View(spiwor);
        }

        // 3. EDYCJA (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POPRAWKA 2: Zmiana typu 'masa' z double na int
        public IActionResult Edit(int id, string model, int temperatura, int masa, decimal cena, int producentId)
        {
            var spiwor = _repo.PobierzSpiwory().FirstOrDefault(s => s.Id == id);

            if (spiwor == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                spiwor.Model = model;
                spiwor.Temperatura = temperatura;
                spiwor.Masa = masa;
                spiwor.Cena = cena;
                spiwor.ProducentId = producentId;

                _repo.EdytujSpiwor(spiwor);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Producenci = _repo.PobierzProducentow();
            return View(spiwor);
        }

        // 4. USUWANIE (GET)
        public IActionResult Delete(int id)
        {
            var spiwor = _repo.PobierzSpiwory().FirstOrDefault(s => s.Id == id);

            if (spiwor == null)
            {
                return NotFound();
            }

            return View(spiwor);
        }

        // 4. USUWANIE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // POPRAWKA 3: Przekazujemy samo ID, a nie cały obiekt
            // (wcześniej było _repo.UsunSpiwor(spiwor), co powodowało błąd)

            _repo.UsunSpiwor(id);

            return RedirectToAction(nameof(Index));
        }
    }
}