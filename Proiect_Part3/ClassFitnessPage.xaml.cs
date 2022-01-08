using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_Part3.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proiect_Part3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassFitnessPage : ContentPage
    {
        ShopList sl;
        public ClassFitnessPage(ShopList slist)
        {
            InitializeComponent();
            sl = slist;
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var product = (ClassFitness)BindingContext;
            await App.Database.SaveProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var product = (ClassFitness)BindingContext;
            await App.Database.DeleteProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            ClassFitness p;
            if (e.SelectedItem != null)
            {
                p = e.SelectedItem as ClassFitness;
                var lp = new ListClass()
                {
                    ShopListID = sl.ID,
                    ClassFitnessID = p.ID
                };
                await App.Database.SaveListClassAsync(lp);
                p.ListClass = new List<ListClass> { lp };

                await Navigation.PopAsync();
            }
        }
    }
}