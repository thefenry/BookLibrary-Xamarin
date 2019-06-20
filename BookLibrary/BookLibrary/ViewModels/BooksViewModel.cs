using BookLibrary.Models;
using BookLibrary.Views;
using System;
using System.Collections.Generic;
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
            get => _books;
            set
            {
                SetProperty(ref _books, value);
            }
        }
        //public ObservableCollection<Book> Books { get; set; }

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

                    Book _book = book as Book;
                    if (_book.Id == 0)
                    {
                        await App.BookRepository.Insert(_book);
                    }
                    else
                    {
                        await App.BookRepository.Update(_book);
                    }

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

                List<Book> books = await App.BookRepository.Get();

                //if (!books.Any())
                //{
                //    books = ReadSeedJson.GetSeedData();
                //    if (books.Any())
                //    {
                //        foreach (Book book in books)
                //        {
                //            await App.BookRepository.Insert(book);
                //        }
                //    }
                //}

                books = books.OrderBy(x => x.Series).ToList();
                foreach (Book book in books)
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

        private async Task ExecuteSearchBooksCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Books.Clear();

                List<Book> books = await App.BookRepository.Get<Book>(x => x.Title.ToLower().Contains(SearchText.ToLower())
                || x.Author.ToLower().Contains(SearchText.ToLower()));

                foreach (Book book in books)
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

        public void ExecuteSortBooksCommand(string sortType)
        {
            List<Book> tempBooks;

            switch (sortType)
            {
                case "Author":
                    tempBooks = Books.OrderBy(x => x.Author).ToList();
                    break;
                case "Title":
                    tempBooks = Books.OrderBy(x => x.Title).ToList();
                    break;
                default:
                    tempBooks = Books.OrderBy(x => x.Series).ToList();
                    break;
            }

            Books.Clear();
            Books = new ObservableCollection<Book>(tempBooks);
        }
    }
}
