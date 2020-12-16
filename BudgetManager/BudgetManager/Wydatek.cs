using System;
using SQLite;

namespace BudgetManager
{
    class Wydatek
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        
        [NotNull]
        public double kwota { get; set; }

        [NotNull]
        public string kategoria { get; set; }

        public string opis { get; set; }

        [NotNull]
        public DateTime data { get; set; }

        [NotNull]
        public string rodzaj { get; set; }

        public Wydatek()
        {
        }
        public Wydatek(double kwota, string kategoria, string opis, DateTime data, string rodzaj)
        {
            this.kwota = kwota;
            this.kategoria = kategoria;
            this.opis = opis;
            this.data = data;
            this.rodzaj = rodzaj;
            
        }
    }
}
