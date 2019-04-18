using BookLibrary.Models;
using BookLibrary.ViewModels;
using BookLibrary.Views.Movies;
using Xamarin.Forms;

namespace MovieLibrary.ViewModels
{
    public class MovieDetailViewModel : BaseViewModel
    {
        Movie movie;
        public Movie Movie
        {
            get => movie;
            set
            {
                movie = value;
                RaisePropertyChanged("Movie");
            }
        }


        public MovieDetailViewModel(Movie movie = null)
        {
            Movie = movie;

            MessagingCenter.Subscribe<AddMovie, Movie>(this, "AddOrUpdateMovie", (obj, movieUpdate) =>
            {
                if (movieUpdate != null)
                {
                    Movie = movieUpdate as Movie;
                }
            });
        }
    }
}
