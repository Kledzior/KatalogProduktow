using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows; // Potrzebne do MessageBox
using System.Windows.Input;
using Kledzik.KatalogSpiworow.BL;
using Kledzik.KatalogSpiworow.Interfaces;
using Kledzik.KatalogSpiworow.Core;

namespace Kledzik.KatalogSpiworow.UI.ViewModels
{
    public class EdycjaViewModel : INotifyPropertyChanged
    {
        // POLA PRYWATNE
        private LogikaKatalogu _logika; 

        // WŁAŚCIWOŚCI
        public ISpiwor Spiwor { get; set; }

        private List<IProducent> _producenci;
        public List<IProducent> Producenci
        {
            get => _producenci;
            set { _producenci = value; OnPropertyChanged(); }
        }

        public Array RodzajeWypelnienia => Enum.GetValues(typeof(RodzajWypelnienia));

        private IProducent _wybranyProducent;
        public IProducent WybranyProducent
        {
            get => _wybranyProducent;
            set
            {
                _wybranyProducent = value;
                if (Spiwor != null && value != null)
                {
                    Spiwor.ProducentId = value.Id;
                }
                OnPropertyChanged();
            }
        }

        // KOMENDY
        public ICommand ZapiszCommand { get; }
        public ICommand DodajProducentaCommand { get; } // <--- NOWE

        public Action ZamknijOkno { get; set; }

        // KONSTRUKTOR
        public EdycjaViewModel(ISpiwor spiwor, LogikaKatalogu logika)
        {
            _logika = logika; 
            Spiwor = spiwor;
            Producenci = logika.PobierzWszystkichProducentow();

            WybranyProducent = Producenci.FirstOrDefault(p => p.Id == spiwor.ProducentId);

            ZapiszCommand = new RelayCommand(Zapisz);
            DodajProducentaCommand = new RelayCommand(DodajProducenta); 
        }

        // METODA DODAWANIA PRODUCENTA
        private void DodajProducenta(object obj)
        {
            // 1. Otwieramy okienko dialogowe
            var dialog = new DodajProducentaWindow();

            if (dialog.ShowDialog() == true)
            {
                string nowaNazwa = dialog.NazwaProducenta;

                // 2. Tworzymy obiekt (używając BL)
                var nowyProducent = _logika.UtworzNowegoProducenta();
                nowyProducent.Nazwa = nowaNazwa;

                // 3. Zapisujemy w bazie
                _logika.DodajProducenta(nowyProducent);

                // 4. Odświeżamy listę w UI
                Producenci = _logika.PobierzWszystkichProducentow();

                // 5. Wybieramy nowego producenta na liście
                WybranyProducent = Producenci.FirstOrDefault(p => p.Id == nowyProducent.Id);
            }
        }

        private void Zapisz(object obj)
        {
            if (Spiwor is IDataErrorInfo info)
            {
                if (!string.IsNullOrEmpty(info["Cena"]) ||
                    !string.IsNullOrEmpty(info["Masa"]) ||
                    !string.IsNullOrEmpty(info["Temperatura"]) ||
                    !string.IsNullOrEmpty(info["Model"]) ||
                    !string.IsNullOrEmpty(info["ProducentId"]))
                {
                    MessageBox.Show("Popraw błędy w formularzu przed zapisaniem!", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            ZamknijOkno?.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}