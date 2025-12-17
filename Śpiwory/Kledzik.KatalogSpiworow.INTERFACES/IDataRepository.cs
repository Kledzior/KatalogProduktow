using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kledzik.KatalogSpiworow.Interfaces
{
    public interface IDataRepository
    {
        // --- Produkty (Śpiwory) ---
        void DodajSpiwor(ISpiwor spiwor);
        void EdytujSpiwor(ISpiwor spiwor);
        void UsunSpiwor(int id);
        List<ISpiwor> PobierzSpiwory();
        // --- Metody pomocnicze (tzw. Fabryki) ---
        // Potrzebne, bo warstwa graficzna (UI) nie będzie znała konkretnych klas, tylko interfejsy
        ISpiwor UtworzNowySpiwor();
        // --- Producenci ---
        void DodajProducenta(IProducent producent);
        void EdytujProducenta(IProducent producent);
        void UsunProducenta(int id);
        List<IProducent> PobierzProducentow();
        IProducent UtworzNowegoProducenta(); // Fabryka dla UI


    }
}
