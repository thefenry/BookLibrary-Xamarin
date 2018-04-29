using BookLibrary.Helpers;
using BookLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookList : ContentPage
    {
        public ObservableCollection<Book> Books { get; set; }

        private string bookFileName = "mybooks.json";

        public BookList()
        {
            InitializeComponent();


            Books = new ObservableCollection<Book>
            {
                new Book
                {
                    Title = "Pride & Prejudice",
                    Author = "Jane Austen"
                },
                new Book
                {
                    Title = "Hitchhickers Guide to the Galaxy",
                    Author = "Douglas Adams"
                }
            };

            string content = JsonConvert.SerializeObject(Books);

            bookFileName.WriteFileAsync(content);

            MyListView.ItemsSource = Books;

        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
