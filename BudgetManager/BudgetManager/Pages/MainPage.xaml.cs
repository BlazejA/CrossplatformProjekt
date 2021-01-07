using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BudgetManager
{
    public partial class MainPage : ContentPage
    {

        List<Wydatek> expenseList = new List<Wydatek>();
        List<Wydatek> incomeList = new List<Wydatek>();
        SQLiteConnection dataBase = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Wydatek.cs"));
        public MainPage()
        {
            InitializeComponent();            
            incomeLayout.IsVisible = false;
            showBudget();            
        }

        private void separateObjects()
        {
            expenseList.Clear();
            incomeList.Clear();
            var table = dataBase.Table<Wydatek>();
            foreach (Wydatek w in table)
            {
                if (w.rodzaj == "WYDATEK")
                    expenseList.Add(w);
                else
                    incomeList.Add(w);
            }
        }
        private void showBudget()
        {
            separateObjects();
            if (expenseLayout.IsVisible) 
                expenseListView.ItemsSource = expenseList;
            else
                incomeListView.ItemsSource = incomeList;
            sumMoney();
        }
        private void sumMoney()
        {
            double sum = 0;
            if (expenseLayout.IsVisible)
            {
                for (int i = 0; i < expenseList.Count; i++)
                {
                    sum += expenseList[i].kwota;
                }
                summaryLabel.Text = "Twoje wydatki: " + sum.ToString() + "PLN";
                summaryLabel.TextColor = Color.FromHex("#993300");
            }
            else
            {
                for (int i = 0; i < incomeList.Count; i++)
                {
                    sum += incomeList[i].kwota;
                }
                summaryLabel.Text = "Twoje przychody: " + sum.ToString() + "PLN";
                summaryLabel.TextColor = Color.FromHex("#006600");
            }
        }
        private void deleteBtn_Clicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            string id = button.ClassId;
            dataBase.Delete<Wydatek>(id);
            showBudget();
        }
        

        //Nawigacja
        private void expenseBtn_Clicked(object sender, EventArgs e)
        {
            expenseLayout.IsVisible = true;
            incomeLayout.IsVisible = false;
            showBudget();
        }

        private void incomeBtn_Clicked(object sender, EventArgs e)
        {
            incomeLayout.IsVisible = true;
            expenseLayout.IsVisible = false;
            showBudget();
        }
       
        private async void addBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Pages.addPage());
        }

        private async void editBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Pages.EditPage());
        }
    }
}
