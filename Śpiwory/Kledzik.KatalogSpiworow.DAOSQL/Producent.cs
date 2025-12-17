using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kledzik.KatalogSpiworow.Interfaces;

namespace Kledzik.KatalogSpiworow.DAOSQL
{
    public class Producent : IProducent
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
    }
}
