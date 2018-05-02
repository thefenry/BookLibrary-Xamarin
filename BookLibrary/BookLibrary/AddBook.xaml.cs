using BookLibrary.Helpers;
using BookLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookLibrary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBook : ContentPage
    {
        public Book Book { get; set; }

        public AddBook()
        {
            InitializeComponent();

            Book = new Book();

            BindingContext = this;
        }

        async void OnCreateClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddBook", Book);
            await Navigation.PopModalAsync();

        }

    }
}