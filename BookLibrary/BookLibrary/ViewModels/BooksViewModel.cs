using BookLibrary.Helpers;
using BookLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookLibrary.ViewModels
{
    public class BooksViewModel
    {

        private string bookFileName = "mybooks.json";

        public ObservableCollection<Book> Books { get; set; }

        public Command LoadBooksCommand { get; set; }

        public BooksViewModel()
        {
            Books = new ObservableCollection<Book>();

            LoadBooksCommand = new Command(async () => await ExecuteLoadBooksCommand());

            MessagingCenter.Subscribe<AddBook, Book>(this, "AddBook", async (obj, book) =>
            {
                if (book != null)
                {

                    var _book = book as Book;

                    Books.Add(_book);

                    var bookLibrary = await bookFileName.ReadAllTextAsync();

                    ObservableCollection<Book> books
                        = JsonConvert.DeserializeObject<ObservableCollection<Book>>(bookLibrary);

                    books.Add(book);

                    string content = JsonConvert.SerializeObject(books);

                    await bookFileName.WriteFileAsync(content);
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

                var bookLibrary = await bookFileName.ReadAllTextAsync();


                var books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(bookLibrary);

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

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
           [CallerMemberName]string propertyName = "",
           Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
