using BookLibrary.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookDetailPage : ContentPage
	{
        BookDetailViewModel viewModel;

        public BookDetailPage (BookDetailViewModel viewModel)
		{
			InitializeComponent ();

            BindingContext = this.viewModel = viewModel;
        }
	}
}