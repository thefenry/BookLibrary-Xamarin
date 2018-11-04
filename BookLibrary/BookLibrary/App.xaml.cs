using BookLibrary.DAL;
using BookLibrary.Interfaces;

using Xamarin.Forms;

namespace BookLibrary
{
	public partial class App : Application
	{
        static BookRepository bookRepository;

		public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new BookLibrary.Views.BookList());//.MainTabPage();
        }

        public static BookRepository Database
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new BookRepository(DependencyService.Get<IFileHelper>().GetLocalFilePath("BookSQLite.db3"));
                }
                return bookRepository;
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
