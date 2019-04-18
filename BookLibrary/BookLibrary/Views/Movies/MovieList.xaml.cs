using BookLibrary.Models;
using BookLibrary.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views.Movies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieList : ContentPage
    {
        MoviesViewModel moviesViewModel;
        public bool showFilter = true;

        public MovieList()
        {
            InitializeComponent();

            BindingContext = moviesViewModel = new MoviesViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Movie item = args.SelectedItem as Movie;
            if (item == null)
                return;

            //await Navigation.PushAsync(new MovieDetailPage(item));

            //// Manually deselect item.
            //MoviesListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddMovie()));
        }

        public async void Sort_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Sort By: ", "Cancel", null, "Author", "Title", "Series");

            if (action != "Cancel")
            {
                moviesViewModel.ExecuteSortMoviesCommand(action);
            }

            SortLabel.Text = $"Sorted by: {action}";
        }

        public void Search_Clicked(object sender, EventArgs e)
        {
            MovieSearch.IsVisible = !MovieSearch.IsVisible;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            moviesViewModel.LoadMoviesCommand.Execute(null);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //thats all you need to make a search  
            string searchText = e.NewTextValue;

            if (string.IsNullOrEmpty(searchText))
            {
                moviesViewModel.LoadMoviesCommand.Execute(null);

            }

            else
            {
                moviesViewModel.SearchText = searchText;
                moviesViewModel.SearchMoviesCommand.Execute(null);

            }
        }

    }
}