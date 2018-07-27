using BookLibrary.Helpers;
using BookLibrary.Models;
using BookLibrary.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookLibrary.ViewModels
{
    public class BooksViewModel : BaseViewModel
    {

        private ObservableCollection<Book> _books;

        public string SearchText { get; set; }

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

        public Command SearchBooksCommand { get; set; }


        public BooksViewModel()
        {
            Books = new ObservableCollection<Book>();

            LoadBooksCommand = new Command(async () => await ExecuteLoadBooksCommand());
            SearchBooksCommand = new Command(async () => await ExecuteSearchBooksCommand());

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

        private async Task ExecuteLoadBooksCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Books.Clear();

                var books = App.Database.GetBooksAsync().Result;

                if (!books.Any())
                {
                    books = ReadSeedJson.GetSeedData();
                    if (books.Any())
                    {
                        await App.Database.SaveBookBatchAsync(books);
                    }
                }

                foreach (var book in books)
                {
                    if (book != null)
                    {
                        Books.Add(book);
                    }
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
            }
        }

        private async Task ExecuteSearchBooksCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Books.Clear();

                var books = await App.Database.SearchBooksAsync(SearchText);

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
