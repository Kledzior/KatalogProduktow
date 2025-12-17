using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq; // Ważne do filtrowania!
using System.Runtime.CompilerServices;
using Kledzik.KatalogSpiworow.BL;
using Kledzik.KatalogSpiworow.Interfaces;
using System.Windows.Input;

namespace Kledzik.KatalogSpiworow.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private LogikaKatalogu _logika;

        // Kopia wszystkich danych (żeby nie stracić ich przy filtrowaniu)
        private List<ISpiwor> _wszystkieSpiwory;

        // Lista widoczna w oknie
        private ObservableCollection<ISpiwor> _spiwory;
        public ObservableCollection<ISpiwor> Spiwory
        {
            get => _spiwory;
            set
            {
                _spiwory = value;
                OnPropertyChanged();
            }
        }

        // Tekst wpisywany w wyszukiwarkę
        private string _tekstFiltra;
        public string TekstFiltra
        {
            get => _tekstFiltra;
            set
            {
                _tekstFiltra = value;
                OnPropertyChanged();
                FiltrujDane(); // Automatycznie filtrujemy przy każdej zmianie litery!
            }
        }

        public ICommand UsunCommand { get; }
        public ICommand DodajCommand { get; private set; }
        public ICommand EdytujCommand { get; private set; }
        public MainViewModel(string nazwaDll)
        {
            _logika = new LogikaKatalogu(nazwaDll);
            UsunCommand = new RelayCommand(UsunSpiwor);
            DodajCommand = new RelayCommand(DodajNowy);
            EdytujCommand = new RelayCommand(EdytujIstniejacy);

            // Pobieramy dane raz
            var dane = _logika.PobierzWszystkieSpiwory();
            _wszystkieSpiwory = new List<ISpiwor>(dane);

            // Na start wyświetlamy wszystko
            Spiwory = new ObservableCollection<ISpiwor>(_wszystkieSpiwory);
        }

        private void UsunSpiwor(object parametr)
        {
            if(parametr is ISpiwor spiworDoUsuniecia)
            {
                // 1. Usuwamy z bazy (przez Logikę)
                _logika.UsunSpiwor(spiworDoUsuniecia.Id);
                // 2. Usuwamy z lokalnej listy (tej wyświetlanej)
                Spiwory.Remove(spiworDoUsuniecia);
                // 3. Usuwamy z kopii zapasowej (żeby nie wrócił przy filtrowaniu)
                _wszystkieSpiwory.Remove(spiworDoUsuniecia);
            }
        }

        private void DodajNowy(object obj)
        {
            // 1. Tworzymy pysty obiekt
            var nowySpiwor = _logika.UtworzNowySpiwor();

            // 2. Otwieramy okno edycji
            if (OtworzOknoEdycji(nowySpiwor))
            {
                // 3. Jeśli użytkownik kliknął Zapisz -> wysyłamy do bazy
                _logika.DodajSpiwor(nowySpiwor);

                // 4. Odświeżamy widok
                _wszystkieSpiwory.Add(nowySpiwor);
                FiltrujDane(); // To odświeży listę Spiwory
            }
        }

        private void EdytujIstniejacy(object obj)
        {
            if (obj is ISpiwor spiworDoEdycji)
            {
                // ⚠️ WAŻNE: Robimy kopię danych przed edycją!
                // Gdybyśmy edytowali oryginał, zmiany widoczne byłyby w tabeli 
                // nawet jak użytkownik kliknie "Anuluj".

                // Tutaj dla uproszczenia (bo DAOMock jest w pamięci) edytujemy "na żywo", 
                // ale w profesjonalnym podejściu klonuje się obiekt.
                // Przyjmijmy wersję prostą:

                if (OtworzOknoEdycji(spiworDoEdycji))
                {
                    // Zapisz zmiany w bazie
                    _logika.EdytujSpiwor(spiworDoEdycji);
                    FiltrujDane(); // Odśwież widok
                }
            }
        }


        private bool OtworzOknoEdycji(ISpiwor spiwor)
        {
            var vm = new EdycjaViewModel(spiwor, _logika);
            var okno = new EdycjaWindow();

            okno.PrzypiszViewModel(vm);

            // ShowDialog zwraca true tylko jak ustawimy DialogResult = true (w przycisku Zapisz)
            return okno.ShowDialog() == true;
        }
        private void FiltrujDane()
        {
            if (string.IsNullOrWhiteSpace(TekstFiltra))
            {
                // Jak filtr pusty, pokaż wszystko
                Spiwory = new ObservableCollection<ISpiwor>(_wszystkieSpiwory);
            }
            else
            {
                // Szukamy w Modelu (ignorując wielkość liter)
                var przefiltrowane = _wszystkieSpiwory
                    .Where(x => x.Model != null &&
                                x.Model.ToLower().Contains(TekstFiltra.ToLower()))
                    .ToList();

                Spiwory = new ObservableCollection<ISpiwor>(przefiltrowane);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}