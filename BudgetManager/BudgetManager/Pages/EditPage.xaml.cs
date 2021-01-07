using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        SQLiteConnection categoriesBase = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                         "Categories.cs"));
        public EditPage()
        {
            InitializeComponent();
            categoriesBase.CreateTable<Categories>();
        }
        private async void mainPageBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }

        private async void addBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Pages.addPage());
        }

        private void addCategory_Completed(object sender, EventArgs e)
        {
            string newCategory = addCategoryEntry.Text;
            Categories cat = new Categories(newCategory);
            if (newCategory != "")
            {
                categoriesBase.Insert(cat);
                successLabel.Text = "Dodano kategorię: " + newCategory;
            }
        }
    }
}