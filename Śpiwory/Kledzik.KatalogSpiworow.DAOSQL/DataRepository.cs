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


            _context.Database.EnsureCreated();
        }

        // --- PRODUCENT ---

        public List<IProducent> PobierzProducentow()
        {
            return _context.Producenci.ToList<IProducent>();
        }

        public IProducent UtworzNowegoProducenta()
        {
            return new Producent();
        }

        public void DodajProducenta(IProducent producent)
        {
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
                bool jestUzywany = _context.Spiwory.Any(s => s.ProducentId == id);
                if (jestUzywany)
                {
        
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
