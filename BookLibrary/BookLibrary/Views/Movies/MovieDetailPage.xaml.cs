using BookLibrary.Models;
using MovieLibrary.ViewModels;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views.Movies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailPage : ContentPage
    {
        MovieDetailViewModel _movie;

        public MovieDetailPage(Movie movie)
        {
            InitializeComponent();
            this._movie = new MovieDetailViewModel(movie);

            BindingContext = this._movie;
        }

        async void EditItem_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddMovie(this._movie.Movie)));
        }

        async void DeleteItem_Clicked(object sender, EventArgs args)
        {
            await App.MoviesRepository.Delete(this._movie.Movie);
            await Navigation.PopToRootAsync();
        }
    }
}