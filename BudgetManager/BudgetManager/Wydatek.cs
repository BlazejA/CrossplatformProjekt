using System;
using SQLite;

namespace BudgetManager
{
    class Wydatek
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        
        [NotNull]
        public int kwota { get; set; }

        [NotNull]
        public string kategoria { get; set; }

        [NotNull]
        public string opis { get; set; }

        public DateTime data { get; set; }

        public Wydatek()
        {
        }
        public Wydatek(int kwota, string kategoria, string opis, DateTime data)
        {
            this.kwota = kwota;
            this.kategoria = kategoria;
            this.opis = opis;
            this.data = data;
        }
    }
}
