using BookLibrary.DAL;
using BookLibrary.Interfaces;
using BookLibrary.Models;
using BookLibrary.Views;
using SQLite;
using Xamarin.Forms;

namespace BookLibrary
{
    public partial class App : Application
    {
        private static SQLiteAsyncConnection database;
        private static LibraryRepository<Book> bookRepository;
        private static LibraryRepository<Movie> moviesRepository;

        public App()
        {
            InitializeComponent();

            string dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("BookSQLite.db3");
            database = new SQLiteAsyncConnection(dbPath);
            //database.CreateTablesAsync<Book, Movie>().Wait();

            MainPage = new MainPage();// NavigationPage(new BookLibrary.Views.BookList());//.MainTabPage();
        }

        public static LibraryRepository<Book> BookRepository
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new LibraryRepository<Book>(database);
                }
                return bookRepository;
            }
        }

        public static LibraryRepository<Movie> MoviesRepository
        {
            get
            {
                if (moviesRepository == null)
                {
                    moviesRepository = new LibraryRepository<Movie>(database);
                }
                return moviesRepository;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
