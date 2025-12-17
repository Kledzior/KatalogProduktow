using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kledzik.KatalogSpiworow.DAOSQL
{
    internal class KatalogContext : DbContext
    {
        public DbSet<Spiwor> Spiwory { get; set; }
        public DbSet<Producent> Producenci { get; set; }

        // Konfiguracja połączenia
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Baza utworzy się w pliku "katalog.db" obok pliku .exe aplikacji
            optionsBuilder.UseSqlite("Data Source=katalog.db");
        }
    }
}
