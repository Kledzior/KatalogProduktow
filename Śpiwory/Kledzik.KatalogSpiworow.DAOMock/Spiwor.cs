using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Kledzik.KatalogSpiworow.Core;
using Kledzik.KatalogSpiworow.Interfaces;

namespace Kledzik.KatalogSpiworow.DAOMock
{
    public class Spiwor : ISpiwor, IDataErrorInfo
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int ProducentId { get; set; }
        public int Masa { get; set; }
        public double Temperatura { get; set; }
        public double Objetosc { get; set; }
        public RodzajWypelnienia Rodzaj { get; set; }
        public decimal Cena { get; set; }


        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string wynik = null;

                switch (columnName)
                {
                    case nameof(Cena):
                        if (Cena <= 0)
                            wynik = "Cena musi być większa od 0 zł.";
                        break;

                    case nameof(Masa):
                        if (Masa <= 0)
                            wynik = "Masa musi być dodatnia.";
                        break;

                    case nameof(Temperatura):
                        if (Temperatura < -50 || Temperatura > 50)
                            wynik = "Temperatura musi być w zakresie od -50 do +50 stopni.";
                        break;

                    case nameof(Model):
                        if (string.IsNullOrWhiteSpace(Model))
                            wynik = "Model nie może być pusty.";
                        else if (Model.Length < 3)
                            wynik = "Nazwa modelu jest za krótka.";
                        break;

                    case nameof(ProducentId):
                        if (ProducentId <= 0)
                            wynik = "Musisz wybrać producenta!";
                        break;

                }

                return wynik;
            }
        }
    }
}