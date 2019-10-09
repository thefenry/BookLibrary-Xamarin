using BookLibrary.Models;
using BookLibrary.Views.Movies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookLibrary.ViewModels
{
    public class MoviesViewModel : BaseViewModel
    {

        private ObservableCollection<Movie> _movies;

        //public ILibraryRepository<Item> DataStore => DependencyService.Get<ILibraryRepository<Item>>() ?? new LibraryRepository();

        bool isLoadingMovies = false;
        public bool IsLoadingMovies
        {
            get { return isLoadingMovies; }
            set { SetProperty(ref isLoadingMovies, value); }
        }

        public string SearchText { get; set; }

        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set
            {
                SetProperty(ref _movies, value);
            }
        }

        public Command LoadMoviesCommand { get; set; }

        public Command SearchMoviesCommand { get; set; }

        public MoviesViewModel()
        {
            Movies = new ObservableCollection<Movie>();

            LoadMoviesCommand = new Command(async () => await ExecuteLoadMoviesCommand());
            SearchMoviesCommand = new Command(async () => await ExecuteSearchMoviesCommand());

            MessagingCenter.Subscribe<AddMovie, Movie>(this, "AddOrUpdateMovie", async (obj, movie) =>
            {
                if (movie != null)
                {

                    Movie _movie = movie as Movie;
                    if (_movie.MovieId == 0)
                    {
                        await App.MoviesRepository.Insert(_movie);
                    }
                    else
                    {
                        await App.MoviesRepository.Update(_movie);
                    }

                    Movies.Add(_movie);
                }
            });
        }

        public async Task ExecuteLoadMoviesCommand()
        {
            if (IsLoadingMovies)
                return;

            IsLoadingMovies = true;

            try
            {
                Movies.Clear();

                List<Movie> movies = await App.MoviesRepository.Get();

                movies = movies.OrderBy(x => x.Title).ToList();
                foreach (Movie movie in movies)
                {
                    if (movie != null)
                    {
                        Movies.Add(movie);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsLoadingMovies = false;
            }
        }

        private async Task ExecuteSearchMoviesCommand()
        {
            if (IsLoadingMovies)
                return;

            IsLoadingMovies = true;

            try
            {
                Movies.Clear();

                List<Movie> movies = await App.MoviesRepository.Get<Movie>(x => x.Title.ToLower().Contains(SearchText.ToLower())
                || x.Description.ToLower().Contains(SearchText.ToLower()));

                foreach (Movie movie in movies)
                {
                    if (movie != null)
                    {
                        Movies.Add(movie);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsLoadingMovies = false;
            }
        }

        public void ExecuteSortMoviesCommand(string sortType)
        {
            List<Movie> tempMovies;

            switch (sortType)
            {
                case "Year":
                    tempMovies = Movies.OrderBy(x => x.Year).ToList();
                    break;
                case "Title":
                    tempMovies = Movies.OrderBy(x => x.Title).ToList();
                    break;
                default:
                    tempMovies = Movies.OrderBy(x => x.Genre).ToList();
                    break;
            }

            Movies.Clear();
            Movies = new ObservableCollection<Movie>(tempMovies);
        }
    }
}
