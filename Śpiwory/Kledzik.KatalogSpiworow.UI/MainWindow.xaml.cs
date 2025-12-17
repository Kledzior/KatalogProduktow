using System.Configuration;
using System.Windows;
using Kledzik.KatalogSpiworow.UI.ViewModels; // Musi widzieć folder ViewModels

namespace Kledzik.KatalogSpiworow.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 1. Odczytujemy nazwę DLL z Configa
            string nazwaDll = ConfigurationManager.AppSettings["DaoLibrary"];

            // 2. Tworzymy ViewModel i przekazujemy mu nazwę DLL
            // 3. Przypisujemy go jako "Kontekst Danych" (DataContext) okna
            this.DataContext = new MainViewModel(nazwaDll);
        }
    }
}