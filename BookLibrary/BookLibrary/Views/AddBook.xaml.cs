using BookLibrary.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBook : ContentPage
    {
        public Book Book { get; set; }

        public AddBook(Book book = null)
        {
            InitializeComponent();

            Book = book == null ? new Book() : book;

            BindingContext = this;
        }

        async void OnCreateClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddOrUpdateBook", Book);
            await Navigation.PopModalAsync();
        }

        async void OnScan_Clicked(object sender, EventArgs eventArgs)
        {
            try
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                    Console.WriteLine("Scanned Barcode: " + result.Text);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}