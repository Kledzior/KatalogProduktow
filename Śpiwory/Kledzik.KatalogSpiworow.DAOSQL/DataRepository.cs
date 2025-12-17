using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kledzik.KatalogSpiworow.Interfaces;

namespace Kledzik.KatalogSpiworow.DAOSQL
{
    public class DataRepository : IDataRepository
    {
        private KatalogContext _context;

        public DataRepository()
        {
            _context = new KatalogContext();

            // To magiczna linijka dla projektów studenckich:
            // "Jeśli plik bazy nie istnieje -> stwórz go i utwórz tabelki".
            _context.Database.EnsureCreated();
        }

        // --- PRODUCENT ---

        public List<IProducent> PobierzProducentow()
        {
            // Pobieramy z bazy i rzutujemy na interfejs
            return _context.Producenci.ToList<IProducent>();
        }

        public IProducent UtworzNowegoProducenta()
        {
            return new Producent();
        }

        public void DodajProducenta(IProducent producent)
        {
            // Rzutowanie na konkretną klasę (bo EF obsługuje klasę Producent, a nie interfejs)
            if (producent is Producent p)
            {
                _context.Producenci.Add(p);
                _context.SaveChanges(); // WAŻNE: Bez tego nic się nie zapisze!
            }
        }

        // W klasie DataRepository (DAOSQL)

        public void UsunProducenta(int id)
        {
            // Szukamy producenta
            var doUsuniecia = _context.Producenci.FirstOrDefault(x => x.Id == id);
            if (doUsuniecia != null)
            {
                // Sprawdźmy, czy nie jest używany (opcjonalne, ale zalecane)
                bool jestUzywany = _context.Spiwory.Any(s => s.ProducentId == id);
                if (jestUzywany)
                {
                    // Tutaj w prawdziwej aplikacji rzucilibyśmy wyjątek lub wyświetlili komunikat.
                    // Na potrzeby prostego projektu możemy po prostu nie usuwać.
                    return;
                }

                _context.Producenci.Remove(doUsuniecia);
                _context.SaveChanges();
            }
        }

        public void EdytujProducenta(IProducent producent)
        {
            var oryginal = _context.Producenci.FirstOrDefault(x => x.Id == producent.Id);
            if (oryginal != null)
            {
                oryginal.Nazwa = producent.Nazwa;
                _context.SaveChanges();
            }
        }

        // --- ŚPIWÓR ---

        public List<ISpiwor> PobierzSpiwory()
        {
            return _context.Spiwory.ToList<ISpiwor>();
        }

        public ISpiwor UtworzNowySpiwor()
        {
            return new Spiwor();
        }

        public void DodajSpiwor(ISpiwor spiwor)
        {
            if (spiwor is Spiwor s)
            {
                _context.Spiwory.Add(s);
                _context.SaveChanges();
            }
        }

        public void UsunSpiwor(int id)
        {
            var doUsuniecia = _context.Spiwory.FirstOrDefault(x => x.Id == id);
            if (doUsuniecia != null)
            {
                _context.Spiwory.Remove(doUsuniecia);
                _context.SaveChanges();
            }
        }

        public void EdytujSpiwor(ISpiwor spiwor)
        {
            // W EF Core, jeśli obiekt jest śledzony, zmiany zapiszą się same przy SaveChanges.
            // Ale tutaj przychodzi obiekt z UI, który może nie być śledzony.
            // Najbezpieczniej: pobierz oryginał z bazy i przepisz wartości.

            var oryginal = _context.Spiwory.FirstOrDefault(x => x.Id == spiwor.Id);
            if (oryginal != null)
            {
                oryginal.Model = spiwor.Model;
                oryginal.Cena = spiwor.Cena;
                oryginal.Masa = spiwor.Masa;
                oryginal.Temperatura = spiwor.Temperatura;
                oryginal.Rodzaj = spiwor.Rodzaj;
                oryginal.ProducentId = spiwor.ProducentId;

                _context.SaveChanges();
            }
        }
    }

}
