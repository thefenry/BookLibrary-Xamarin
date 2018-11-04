﻿using BookLibrary.Models;
using BookLibrary.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookList : ContentPage
    {
        BooksViewModel booksViewModel;
        public bool showFilter = true;

        public BookList()
        {
            InitializeComponent();

            BindingContext = booksViewModel = new BooksViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Book;
            if (item == null)
                return;

            await Navigation.PushAsync(new BookDetailPage(item));

            // Manually deselect item.
            BooksListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {           
            await Navigation.PushModalAsync(new NavigationPage(new AddBook()));
        }

        public async void Sort_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Sort By: ", "Cancel", null, "Author","Title","Series");

            if (action != "Cancel")
            {
                booksViewModel.ExecuteSortBooksCommand(action);
            }

            SortLabel.Text = $"Sorted by: {action}";
        }

        public void Search_Clicked(object sender, EventArgs e)
        {
            BookSearch.IsVisible = !BookSearch.IsVisible;
        }
               
        protected override void OnAppearing()
        {
            base.OnAppearing();

            booksViewModel.LoadBooksCommand.Execute(null);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //thats all you need to make a search  
            string searchText = e.NewTextValue;

            if (string.IsNullOrEmpty(searchText))
            {
                booksViewModel.LoadBooksCommand.Execute(null);

                //list.ItemsSource = tempdata;
            }

            else
            {
                booksViewModel.SearchText = searchText;
                booksViewModel.SearchBooksCommand.Execute(null);
                 //booksViewModel.Books.Where(x => x.Title.Contains(searchText) || x.Author.Contains(searchText));

                //list.ItemsSource = tempdata.Where(x => x.Name.StartsWith(e.NewTextValue));
            }
        }

        private void picker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}
