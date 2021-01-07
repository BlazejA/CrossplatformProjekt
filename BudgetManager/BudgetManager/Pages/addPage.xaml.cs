using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class addPage : ContentPage
    {
        SQLiteConnection dataBase = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                         "Wydatek.cs"));
        SQLiteConnection categoriesBase = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                         "Category.cs"));


        public addPage()
        {
            InitializeComponent();
            
            pickCategory.ItemsSource = categoriesList();            
        }

        private List<string> categoriesList()
        {
            categoriesBase.CreateTable<Category>();
            List<string> list = new List<string>();
            var table = categoriesBase.Table<Category>();
            foreach (Category c in table)
            {
                list.Add(c.category);
            }
            if (list.Count == 0)
            {
                pickCategory.Title = "Brak dodanych kategorii!";
            }
            return list;
        }
        private async void mainPageBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }

        private async void editBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditPage());
        }


        private void wydatekBtn_Clicked(object sender, EventArgs e)
        {
            dataBase.CreateTable<Wydatek>();
            if (checkIfDataIsCorrect(moneyAmountEntry.Text, pickCategory.SelectedItem))
            {
                Wydatek newExpense = new Wydatek(double.Parse(moneyAmountEntry.Text),
                                                pickCategory.SelectedItem.ToString(),
                                                descriptionEntry.Text,
                                                datePicker.Date,
                                                "WYDATEK");
                dataBase.Insert(newExpense);
                infoLabel.Text = "Dodano wydatek!";
                moneyAmountEntry.Text = "";
                descriptionEntry.Text = "";
            }

        }

        private bool checkIfDataIsCorrect(string moneyAmount, object pickedCategory)
        {
            if (moneyAmount == null || pickedCategory == null)
            {
                DisplayAlert("Błędne dane", "Uzupełnij wszytkie wymagane dane!", "Ok");
                return false;
            }
            else
            {
                if (double.TryParse(moneyAmount, out double a))
                {
                    return true;
                }
                DisplayAlert("Błędne dane", "Kwota musi być liczbą!", "Ok");
                return false;
            }
        }

        private void przychodBtn_Clicked(object sender, EventArgs e)
        {
            dataBase.CreateTable<Wydatek>();
            if (checkIfDataIsCorrect(moneyAmountEntry.Text, pickCategory.SelectedItem))
            {
                Wydatek newExpense = new Wydatek(double.Parse(moneyAmountEntry.Text),
                                                pickCategory.SelectedItem.ToString(),
                                                descriptionEntry.Text,
                                                datePicker.Date,
                                                "PRZYCHOD");
                dataBase.Insert(newExpense);
                infoLabel.Text = "Dodano przychód!";
                moneyAmountEntry.Text = "";
                descriptionEntry.Text = "";
            }
        }
    }
}