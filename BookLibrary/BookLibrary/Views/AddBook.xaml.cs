using BookLibrary.Models;
using BookLibrary.Services;
using BookLibrary.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace BookLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBook : ContentPage
    {
        public BookDetailViewModel _book;
        private GoogleBooksService googleService = new GoogleBooksService();

        public AddBook(Book book = null)
        {
            InitializeComponent();
            Book editingBook = book ?? new Book();
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
                string isbn = string.Empty;
                ZXingScannerPage scan = new ZXingScannerPage();
                await Navigation.PushAsync(scan);
                scan.OnScanResult += (result) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopAsync();
                        isbn = result.Text;

                        Book bookResult = await googleService.GetBookAsync(isbn);
                        if (bookResult == null)
                        {
                            await DisplayAlert("Could not find Book", "Could not find book. Please check the barcode and try again.", "OK");
                        }
                        else
                        {
                            this._book.Book = bookResult;
                        }
                    });
                };
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Occured", "An Error occured while scanning. Try again later.", "Ok");
                Log.Warning("Scanner", ex.Message);
                throw new Exception(ex.Message, ex);
            }

        }

    }
}