using System;
using System.Collections.Generic;
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
        public EditPage()
        {
            InitializeComponent();
        }
        private async void mainPageBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
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