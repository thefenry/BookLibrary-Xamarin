using BookLibrary.Models;
using MovieLibrary.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views.Movies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMovie : ContentPage
    {
        public MovieDetailViewModel _movie;

        public AddMovie(Movie movie = null)
        {
            InitializeComponent();

            Movie editingMovie = movie == null ? new Movie() : movie;
            this._movie = new MovieDetailViewModel(editingMovie);

            BindingContext = this._movie;
        }

        async void OnCreateClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddOrUpdateMovie", this._movie.Movie);
            await Navigation.PopModalAsync();
        }

        async void OnScan_Clicked(object sender, EventArgs eventArgs)
        {
            //try
            //{
            //    ZXing.Mobile.MobileBarcodeScanner scanner = new ZXing.Mobile.MobileBarcodeScanner();

            //    ZXing.Result result = await scanner.Scan();

            //    if (result != null)
            //    {
            //        Console.WriteLine("Scanned Barcode: " + result.Text);

            //        //var service = new GoogleBooksService();
            //        //this._movie.Movie = await service.GetBookAsync(result.Text);                    //var service = new GoogleBooksService();
            //        //this._movie.Movie = await service.GetBookAsync(result.Text);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("Error Occured", "An Error occured while scanning. Try again later.", "Ok");
            //    Log.Warning("Scanner", ex.Message);
            //}

        }
    }
}