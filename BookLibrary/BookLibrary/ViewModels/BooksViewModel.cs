using BookLibrary.Models;
using BookLibrary.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookLibrary.ViewModels
{
    public class BooksViewModel: BaseViewModel
    {

        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set
            {
                _books = value;
                RaisePropertyChanged("Books");
            }
        }

        public Command LoadBooksCommand { get; set; }

        public BooksViewModel()
        {
            Books = new ObservableCollection<Book>();

            LoadBooksCommand = new Command(async () => await ExecuteLoadBooksCommand());

            MessagingCenter.Subscribe<AddBook, Book>(this, "AddOrUpdateBook", async (obj, book) =>
            {
                if (book != null)
                {

                    var _book = book as Book;

                    await App.Database.SaveBookAsync(_book);

                    Books.Add(_book);
                }
            });

        }

        async Task ExecuteLoadBooksCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Books.Clear();

                var books = await App.Database.GetBooksAsync();

                foreach (var book in books)
                {
                    if (book != null)
                    {
                        Books.Add(book);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }  
    }
}
