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
        List<Przychod> incomeList = new List<Przychod>();
        SQLiteConnection wydatekBase = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                                        "Wydatek.cs"));
        SQLiteConnection przychodBase = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                                         "Przychod.cs"));
        public MainPage()
        {
            InitializeComponent();            
            incomeLayout.IsVisible = false;
            przychodBase.CreateTable<Przychod>();
            wydatekBase.CreateTable<Wydatek>();
            showBudget();
            
        }

        private void separateObjects()
        {
            expenseList.Clear();
            incomeList.Clear();
            var expenseTable = wydatekBase.Table<Wydatek>();
            foreach (Wydatek w in expenseTable)            
                expenseList.Add(w);

            var incomeTable = przychodBase.Table<Przychod>();
            foreach (Przychod p in incomeTable)
                incomeList.Add(p);
        }
        private void showBudget()
        {
            if (expenseLayout.IsVisible) 
                expenseListView.ItemsSource = wydatekBase.Table<Wydatek>().ToList();
            else
                incomeListView.ItemsSource = przychodBase.Table<Przychod>().ToList();
            sumMoney();
        }
        private double sumExpense()
        {
            double sum = 0;
            for (int i = 0; i < expenseList.Count; i++)
            {
                sum += expenseList[i].kwota;
            }
            summaryLabel.Text = "Twoje wydatki: " + sum.ToString() + "PLN";
            summaryLabel.TextColor = Color.FromHex("#993300");
            return sum;
        }
        private double sumIncome()
        {
            double sum = 0;
            for (int i = 0; i < incomeList.Count; i++)
            {
                sum += incomeList[i].kwota;
            }
            summaryLabel.Text = "Twoje przychody: " + sum.ToString() + "PLN";
            summaryLabel.TextColor = Color.FromHex("#006600");
            return sum;
        }
        private void sumMoney()
        {
            separateObjects();            
            if (expenseLayout.IsVisible)
            {
                sumExpense();
            }
            else
            {
                sumIncome();
            }
            double bilans = sumExpense() - sumIncome();
            bilansLabel.Text = "Bilans: " + bilans;
        }
        private void deleteBtn_Clicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            string id = button.ClassId;
            if (expenseLayout.IsVisible)
                wydatekBase.Delete<Wydatek>(id);
            else
                przychodBase.Delete<Przychod>(id);
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
