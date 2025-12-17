using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kledzik.KatalogSpiworow.Core;       // Do Enuma
using Kledzik.KatalogSpiworow.Interfaces;


namespace Kledzik.KatalogSpiworow.DAOMock
{
    public class DataRepository : IDataRepository
    {
        // Tutaj trzymamy dane w pamięci (zamiast w pliku)
        private List<ISpiwor> _spiwory = new List<ISpiwor>();
        private List<IProducent> _producenci = new List<IProducent>();

        public DataRepository()
        {
            ZaladujDaneTestowe();
        }

        private void ZaladujDaneTestowe()
        {
            // 1. TWORZENIE PRODUCENTÓW
            var p1 = new Producent { Id = 1, Nazwa = "Pajak" };
            var p2 = new Producent { Id = 2, Nazwa = "Sea to Summit" };
            var p3 = new Producent { Id = 3, Nazwa = "Robens" };
            var p4 = new Producent { Id = 4, Nazwa = "Thermarest" };
            var p5 = new Producent { Id = 5, Nazwa = "Carinthia" };
            var p6 = new Producent { Id = 6, Nazwa = "Snugpak" };

            _producenci.AddRange(new[] { p1, p2, p3, p4, p5, p6 });

            // 2. TWORZENIE ŚPIWORÓW (Twoje dane z Excela)
            _spiwory.Add(new Spiwor { Id = 1, ProducentId = 1, Model = "Quest 4TWO", Masa = 1100, Temperatura = 5, Objetosc = 5, Rodzaj = RodzajWypelnienia.Puch, Cena = 2359.00m });
            _spiwory.Add(new Spiwor { Id = 2, ProducentId = 2, Model = "Spark -1C/30F", Masa = 536, Temperatura = -1, Objetosc = 8, Rodzaj = RodzajWypelnienia.Puch, Cena = 1719.99m });
            _spiwory.Add(new Spiwor { Id = 3, ProducentId = 1, Model = "Core 550", Masa = 915, Temperatura = -9, Objetosc = 5, Rodzaj = RodzajWypelnienia.Puch, Cena = 1689.00m });
            _spiwory.Add(new Spiwor { Id = 4, ProducentId = 1, Model = "Core 400", Masa = 800, Temperatura = -6, Objetosc = 5, Rodzaj = RodzajWypelnienia.Puch, Cena = 1499.00m });
            _spiwory.Add(new Spiwor { Id = 5, ProducentId = 1, Model = "Quest Blanket", Masa = 420, Temperatura = 9, Objetosc = 2, Rodzaj = RodzajWypelnienia.Puch, Cena = 1039.00m });
            _spiwory.Add(new Spiwor { Id = 6, ProducentId = 1, Model = "Radical 1Z", Masa = 440, Temperatura = 0, Objetosc = 2.2, Rodzaj = RodzajWypelnienia.Puch, Cena = 2199.00m });
            _spiwory.Add(new Spiwor { Id = 7, ProducentId = 1, Model = "Radical ULZ", Masa = 325, Temperatura = 0, Objetosc = 1.4, Rodzaj = RodzajWypelnienia.Puch, Cena = 1779.00m });
            _spiwory.Add(new Spiwor { Id = 8, ProducentId = 1, Model = "Radical ULX", Masa = 190, Temperatura = 9, Objetosc = 1.4, Rodzaj = RodzajWypelnienia.Puch, Cena = 1269.00m });
            _spiwory.Add(new Spiwor { Id = 9, ProducentId = 1, Model = "Radical 4Z", Masa = 720, Temperatura = -7, Objetosc = 5, Rodzaj = RodzajWypelnienia.Puch, Cena = 2999.00m });
            _spiwory.Add(new Spiwor { Id = 10, ProducentId = 1, Model = "Radical 8Z", Masa = 1030, Temperatura = -10, Objetosc = 6, Rodzaj = RodzajWypelnienia.Puch, Cena = 3409.00m });
            _spiwory.Add(new Spiwor { Id = 11, ProducentId = 1, Model = "Radical 12Z", Masa = 1300, Temperatura = -14, Objetosc = 6.5, Rodzaj = RodzajWypelnienia.Puch, Cena = 4139.00m });
            _spiwory.Add(new Spiwor { Id = 12, ProducentId = 1, Model = "Core 250", Masa = 420, Temperatura = 12, Objetosc = 2, Rodzaj = RodzajWypelnienia.Puch, Cena = 1309.00m });
            _spiwory.Add(new Spiwor { Id = 13, ProducentId = 3, Model = "Gully 300", Masa = 875, Temperatura = 4, Objetosc = 1.7, Rodzaj = RodzajWypelnienia.Hybryda, Cena = 555.26m });
            _spiwory.Add(new Spiwor { Id = 14, ProducentId = 4, Model = "Hyperion Regular 0C", Masa = 454, Temperatura = 5, Objetosc = 1, Rodzaj = RodzajWypelnienia.Puch, Cena = 1834.34m });
            _spiwory.Add(new Spiwor { Id = 15, ProducentId = 1, Model = "Quest Quilt", Masa = 485, Temperatura = 5, Objetosc = 2, Rodzaj = RodzajWypelnienia.Puch, Cena = 1089.00m });
            _spiwory.Add(new Spiwor { Id = 16, ProducentId = 3, Model = "Couloir 350", Masa = 795, Temperatura = -4, Objetosc = 2, Rodzaj = RodzajWypelnienia.Puch, Cena = 1101.50m });
            _spiwory.Add(new Spiwor { Id = 17, ProducentId = 5, Model = "Defence 4 Medium", Masa = 1850, Temperatura = -8.8, Objetosc = 4.3, Rodzaj = RodzajWypelnienia.Syntetyk, Cena = 979.00m });
            _spiwory.Add(new Spiwor { Id = 18, ProducentId = 6, Model = "Softie 9 Hawk", Masa = 1500, Temperatura = -5, Objetosc = 3.78, Rodzaj = RodzajWypelnienia.Syntetyk, Cena = 769.00m });
            _spiwory.Add(new Spiwor { Id = 19, ProducentId = 1, Model = "Core 950", Masa = 1400, Temperatura = -18, Objetosc = 10, Rodzaj = RodzajWypelnienia.Puch, Cena = 2239.00m });
            _spiwory.Add(new Spiwor { Id = 20, ProducentId = 5, Model = "SOF 3 Medium", Masa = 1616, Temperatura = -8, Objetosc = 2.3, Rodzaj = RodzajWypelnienia.Syntetyk, Cena = 1259.00m });
        }

        // --- CRUD DLA ŚPIWORÓW ---
        public void DodajSpiwor(ISpiwor spiwor)
        {
            if (spiwor != null)
            {
                spiwor.Id = _spiwory.Any() ? _spiwory.Max(x => x.Id) + 1 : 1;
                _spiwory.Add(spiwor);
            }
        }

        public void EdytujSpiwor(ISpiwor spiwor)
        {
            var istniejacy = _spiwory.FirstOrDefault(x => x.Id == spiwor.Id);
            if (istniejacy != null)
            {
                istniejacy.Model = spiwor.Model;
                istniejacy.Cena = spiwor.Cena;
                istniejacy.Masa = spiwor.Masa;
                istniejacy.Temperatura = spiwor.Temperatura;
                istniejacy.Objetosc = spiwor.Objetosc;
                istniejacy.Rodzaj = spiwor.Rodzaj;
                istniejacy.ProducentId = spiwor.ProducentId;
            }
        }

        public void UsunSpiwor(int id)
        {
            var doUsuniecia = _spiwory.FirstOrDefault(x => x.Id == id);
            if (doUsuniecia != null) _spiwory.Remove(doUsuniecia);
        }

        public List<ISpiwor> PobierzSpiwory()
        {
            return _spiwory;
        }

        public ISpiwor UtworzNowySpiwor()
        {
            return new Spiwor();
        }

        // --- CRUD DLA PRODUCENTÓW ---
        public void DodajProducenta(IProducent producent)
        {
            if (producent != null)
            {
                producent.Id = _producenci.Any() ? _producenci.Max(x => x.Id) + 1 : 1;
                _producenci.Add(producent);
            }
        }

        public void EdytujProducenta(IProducent producent)
        {
            var istniejacy = _producenci.FirstOrDefault(x => x.Id == producent.Id);
            if (istniejacy != null) istniejacy.Nazwa = producent.Nazwa;
        }

        public void UsunProducenta(int id)
        {
            var doUsuniecia = _producenci.FirstOrDefault(x => x.Id == id);
            if (doUsuniecia != null) _producenci.Remove(doUsuniecia);
        }

        public List<IProducent> PobierzProducentow()
        {
            return _producenci;
        }

        public IProducent UtworzNowegoProducenta()
        {
            return new Producent();
        }
    }
}