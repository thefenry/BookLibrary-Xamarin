using BookLibrary.Models;
using BookLibrary.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookDetailPage : ContentPage
	{
        BookDetailViewModel _book;
        //BookDetailViewModel viewModel;

        public BookDetailPage (Book book)
		{
			InitializeComponent ();
            this._book = new BookDetailViewModel(book);

            BindingContext = this._book;
        }

        async void EditItem_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddBook(this._book.Book)));
        }

        async void DeleteItem_Clicked(object sender, EventArgs args)
        {
            await App.BookRepository.Delete(this._book.Book);
            await Navigation.PopToRootAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}