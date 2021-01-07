using System;
using SQLite;

namespace BudgetManager
{
    class Category
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string category { get; set; }

        public Category(){ }

        public Category(string category)
        {
            this.category = category;
        }
    }
}
