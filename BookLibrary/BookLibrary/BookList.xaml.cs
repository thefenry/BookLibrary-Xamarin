using BookLibrary.Models;
using BookLibrary.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary
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

            await Navigation.PushAsync(new BookDetailPage(new BookDetailViewModel(item)));

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

            if (booksViewModel.Books.Count == 0)
                booksViewModel.LoadBooksCommand.Execute(null);


        }
    }
}
