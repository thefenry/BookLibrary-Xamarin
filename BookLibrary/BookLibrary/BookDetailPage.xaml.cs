using BookLibrary.Models;
using BookLibrary.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}