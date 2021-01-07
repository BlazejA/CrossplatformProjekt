using System;
using SQLite;

namespace BudgetManager
{
    
     class Categories
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string category { get; set; }

        public Categories()
        {
        }

        public Categories(string category)
        {
            this.category = category;
        }
    }
}
