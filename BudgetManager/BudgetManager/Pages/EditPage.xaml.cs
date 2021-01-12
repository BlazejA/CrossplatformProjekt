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
                                                         "Category.cs"));
        SQLiteConnection wydatekBase = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                                        "Wydatek.cs"));
        SQLiteConnection przychodBase = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                                         "Przychod.cs"));
        public EditPage()
        {
            InitializeComponent();
            categoriesBase.CreateTable<Category>();
            showCategories();
        }
        private void showCategories()
        {
            categoriesListview.ItemsSource = categoriesBase.Table<Category>().ToList();
        }
        
        private void addCategory_Completed(object sender, EventArgs e)
        {
            string newCategory = addCategoryEntry.Text;
            Category cat = new Category(newCategory);
            if (newCategory != "")
            {
                categoriesBase.Insert(cat);
                successLabel.Text = "Dodano kategorię: " + newCategory;
                categoriesListview.ItemsSource = categoriesBase.Table<Category>().ToList();
            }
        }

        private async void categoriesListview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Category clickedCategory = (Category)e.SelectedItem;
            var alert = await DisplayAlert(clickedCategory.category, "Czy chcesz usunąć wybraną kategorię?", "Tak", "Nie");
            if (alert)
            {                
                categoriesBase.Delete(clickedCategory);
                showCategories();
            }
        }

        
                 public string wydatkiSum { get; }  = "HALO";
        

        private async void mainPageBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }

        private async void addBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Pages.addPage());
        }

    }
}