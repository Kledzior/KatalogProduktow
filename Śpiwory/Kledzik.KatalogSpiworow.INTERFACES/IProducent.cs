using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kledzik.KatalogSpiworow.Interfaces
{
    public interface IProducent
    {
        int Id { get; set; }
        string Nazwa { get; set; }
    }
}
