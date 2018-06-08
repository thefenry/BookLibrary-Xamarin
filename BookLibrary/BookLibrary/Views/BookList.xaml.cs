using BookLibrary.Models;
using BookLibrary.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookList : ContentPage
    {
        BooksViewModel booksViewModel;

        public BookList()
        {
            InitializeComponent();

            BindingContext = booksViewModel = new BooksViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Book;
            if (item == null)
                return;

            await Navigation.PushAsync(new BookDetailPage(item));

            // Manually deselect item.
            BooksListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {           
            await Navigation.PushModalAsync(new NavigationPage(new AddBook()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            booksViewModel.LoadBooksCommand.Execute(null);
        }
    }
}
