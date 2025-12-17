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
using Kledzik.KatalogSpiworow.UI.ViewModels;

namespace Kledzik.KatalogSpiworow.UI
{
    /// <summary>
    /// Logika interakcji dla klasy EdycjaWindow.xaml
    /// </summary>
    public partial class EdycjaWindow : Window
    {
        public EdycjaWindow()
        {
            InitializeComponent();
        }

        public void PrzypiszViewModel(EdycjaViewModel vm)
        {
            this.DataContext = vm;

            vm.ZamknijOkno = () =>
            {
                this.DialogResult = true;
                this.Close();
            };
        }
    }
}
