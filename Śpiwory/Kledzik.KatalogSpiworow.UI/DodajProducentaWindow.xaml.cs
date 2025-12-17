using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kledzik.KatalogSpiworow.UI
{
    public partial class DodajProducentaWindow : Window
    {
        // Właściwość, przez którą pobierzemy wpisaną nazwę
        public string NazwaProducenta { get; private set; }

        public DodajProducentaWindow()
        {
            InitializeComponent();
            txtNazwa.Focus(); // Ustaw kursor od razu w polu
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNazwa.Text))
            {
                MessageBox.Show("Podaj nazwę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NazwaProducenta = txtNazwa.Text;
            DialogResult = true; // To zamyka okno i zwraca true do ShowDialog()
        }

        private void BtnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
