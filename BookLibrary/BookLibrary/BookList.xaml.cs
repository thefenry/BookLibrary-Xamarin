using BookLibrary.Helpers;
using BookLibrary.Models;
using BookLibrary.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookList : ContentPage
    {

        BooksViewModel booksViewModel;
        //private string bookFileName = "mybooks.json";

        public BookList()
        {
            InitializeComponent();

            BindingContext = booksViewModel = new BooksViewModel();

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddBook()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (booksViewModel.Books.Count == 0)
                booksViewModel.LoadBooksCommand.Execute(null);


        }
    }
}
