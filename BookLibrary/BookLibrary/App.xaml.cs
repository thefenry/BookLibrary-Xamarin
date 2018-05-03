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

            Resources = new ResourceDictionary();
            Resources.Add("primaryGreen", Color.FromHex("91CA47"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));
    
            MainPage = new BookLibrary.MainTabPage();

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
