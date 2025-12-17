using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kledzik.KatalogSpiworow.Core;
using System.ComponentModel;

namespace Kledzik.KatalogSpiworow.Interfaces
{
    public interface ISpiwor : IDataErrorInfo
    {
        int Id { get; set; }
        string Model { get; set; }      // Np. "Quest 4TWO"
        // Relacja - ID producenta (Wymaganie 1.7)
        int ProducentId { get; set; }
    
        int Masa { get; set; }          // w gramach
        double Temperatura { get; set; } // w stopniach celsjusza
        double Objetosc { get; set; }    // w litrach



        // Typ wyliczeniowy (Wymaganie 1.8)
        RodzajWypelnienia Rodzaj { get; set; }

        decimal Cena { get; set; }
    }
}
