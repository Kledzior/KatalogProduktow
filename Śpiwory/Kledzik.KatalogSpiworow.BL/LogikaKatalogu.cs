using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text; // Potrzebne do StringBuilder
using Kledzik.KatalogSpiworow.Interfaces;
using Kledzik.KatalogSpiworow.Core;

namespace Kledzik.KatalogSpiworow.BL
{
    public class LogikaKatalogu
    {
        private IDataRepository _repo;

        public LogikaKatalogu(string nazwaBibliotekiDll)
        {
            WczytajBazeDanych(nazwaBibliotekiDll);
        }

        private void WczytajBazeDanych(string nazwaDll)
        {
            // 1. Ustalanie ścieżki
            string sciezka = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nazwaDll);

            if (!File.Exists(sciezka))
            {
                throw new FileNotFoundException($"Nie znaleziono pliku bazy danych: {sciezka}. Upewnij się, że plik .dll jest w folderze bin/Debug.");
            }

            // 2. Ładowanie biblioteki
            var assembly = Assembly.LoadFrom(sciezka);

            // 3. Szukanie klasy (ZABEZPIECZONE)
            Type typRepo = null;

            try
            {
                // To jest ten moment, który wybuchał. Teraz go łapiemy.
                foreach (var typ in assembly.GetTypes())
                {
                    if (typeof(IDataRepository).IsAssignableFrom(typ) && !typ.IsInterface && !typ.IsAbstract)
                    {
                        typRepo = typ;
                        break;
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Budujemy czytelny komunikat o tym, czego brakuje
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Błąd ładowania typów z biblioteki DAO.");
                sb.AppendLine("Prawdopodobnie brakuje zależności (np. EntityFrameworkCore).");
                sb.AppendLine("Szczegóły brakujących plików:");

                foreach (var loaderEx in ex.LoaderExceptions)
                {
                    if (loaderEx != null)
                    {
                        sb.AppendLine($"- {loaderEx.Message}");
                    }
                }

                // Rzucamy nowy błąd z pełnym opisem, żebyś widział go w oknie błędu
                throw new Exception(sb.ToString());
            }

            if (typRepo == null)
            {
                throw new Exception("W podanej bibliotece nie znaleziono klasy implementującej IDataRepository.");
            }

            // 4. Tworzenie instancji
            _repo = (IDataRepository)Activator.CreateInstance(typRepo);
        }

        // ==========================================
        // METODY POŚREDNICZĄCE
        // ==========================================

        // --- ŚPIWORY ---
        public List<ISpiwor> PobierzWszystkieSpiwory() => _repo.PobierzSpiwory();
        public void DodajSpiwor(ISpiwor spiwor) => _repo.DodajSpiwor(spiwor);
        public void EdytujSpiwor(ISpiwor spiwor) => _repo.EdytujSpiwor(spiwor);
        public void UsunSpiwor(int id) => _repo.UsunSpiwor(id);

        // UWAGA: Sprawdź w interfejsie IDataRepository czy metoda nazywa się 'UtworzSpiwor' czy 'UtworzNowySpiwor'.
        // Zazwyczaj nazywaliśmy ją 'UtworzSpiwor'. Dostosuj poniższą linię do swojego interfejsu:
        public ISpiwor UtworzNowySpiwor() => _repo.UtworzNowySpiwor();

        // --- PRODUCENCI ---
        public List<IProducent> PobierzWszystkichProducentow() => _repo.PobierzProducentow();
        public void DodajProducenta(IProducent producent) => _repo.DodajProducenta(producent);

        // UWAGA: Tutaj też sprawdź nazwę w interfejsie (UtworzProducenta vs UtworzNowegoProducenta)
        public IProducent UtworzNowegoProducenta() => _repo.UtworzNowegoProducenta();

        // Dodatkowa metoda dla spójności z ViewModel
        public void DodajNowegoProducenta(IProducent producent) => _repo.DodajProducenta(producent);
    }
}