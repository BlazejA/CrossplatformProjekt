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

            return sum;
        }
        private double sumIncome()
        {
            double sum = 0;
            for (int i = 0; i < incomeList.Count; i++)
            {
                sum += incomeList[i].kwota;
            }

            return sum;
        }
        private void sumMoney()
        {
            separateObjects();
            if (expenseLayout.IsVisible)
            {
                summaryLabel.Text = "Twoje wydatki: " + sumExpense().ToString() + "PLN";
                summaryLabel.TextColor = Color.FromHex("#993300");


            }
            else
            {
                summaryLabel.Text = "Twoje przychody: " + sumIncome().ToString() + "PLN";
                summaryLabel.TextColor = Color.FromHex("#006600");
            }
            double bilans = sumIncome() - sumExpense();
            bilansLabel.Text = "Bilans: " + bilans + "PLN";
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

        private void filterPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (expenseLayout.IsVisible)
            {
                expenseListView.ItemsSource = null;
                expenseList.Clear();
                foreach (var x in wydatekBase.Table<Wydatek>().ToList())
                {
                    if (filterPicker.SelectedIndex == 0)
                    {
                        if (x.data == DateTime.Now.Date)
                            expenseList.Add(x);
                    }
                    else if (filterPicker.SelectedIndex == 1)
                    {
                        if (x.data.Date == DateTime.Now.AddDays(-1).Date)                        
                            expenseList.Add(x);
                    }
                    else if (filterPicker.SelectedIndex == 2)
                    {
                        if (x.data.Date >= DateTime.Now.AddDays(-7).Date && DateTime.Compare(x.data.Date,DateTime.Now.Date.AddDays(1))<0)
                            expenseList.Add(x);
                    }
                    else if (filterPicker.SelectedIndex == 3)
                    {
                        if (x.data.Date >= DateTime.Now.AddDays(-30).Date && DateTime.Compare(x.data.Date, DateTime.Now.Date.AddDays(1)) < 0)
                            expenseList.Add(x);
                    }
                    else
                        showBudget();
                }
                expenseListView.ItemsSource = expenseList;
            }
            else
            {
                incomeListView.ItemsSource = null;
                incomeList.Clear();
                foreach (var x in przychodBase.Table<Przychod>().ToList())
                {
                    if (filterPicker.SelectedIndex == 0)
                    {
                        if (x.data == DateTime.Now.Date)
                            incomeList.Add(x);
                    }
                    else if (filterPicker.SelectedIndex == 1)
                    {
                        if (x.data.Date == DateTime.Now.AddDays(-1).Date)
                            incomeList.Add(x);
                    }
                    else if (filterPicker.SelectedIndex == 2)
                    {
                        if (x.data.Date >= DateTime.Now.AddDays(-7).Date && DateTime.Compare(x.data.Date, DateTime.Now.Date.AddDays(1)) < 0)
                            incomeList.Add(x);
                    }
                    else if (filterPicker.SelectedIndex == 3)
                    {
                        if (x.data.Date >= DateTime.Now.AddDays(-30).Date && DateTime.Compare(x.data.Date, DateTime.Now.Date.AddDays(1)) < 0)
                            incomeList.Add(x);
                    }
                    else
                        showBudget();
                }
                incomeListView.ItemsSource = incomeList;
            }
        }
    }
}

