using BookLibrary.Models;
using BookLibrary.Services;
using BookLibrary.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBook : ContentPage
    {
        public BookDetailViewModel _book;

        public AddBook(Book book = null)
        {
            InitializeComponent();

            Book editingBook = book == null ? new Book() : book;
            this._book = new BookDetailViewModel(editingBook);

            BindingContext = this._book;
        }

        async void OnCreateClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddOrUpdateBook", this._book.Book);
            await Navigation.PopModalAsync();
        }

        async void OnScan_Clicked(object sender, EventArgs eventArgs)
        {
            try
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                {
                    Console.WriteLine("Scanned Barcode: " + result.Text);

                    var service = new GoogleBooksService();
                    this._book.Book = await service.GetBookAsync(result.Text);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}