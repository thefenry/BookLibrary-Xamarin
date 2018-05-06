using BookLibrary.Models;
using BookLibrary.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BookLibrary.ViewModels
{
    public class BookDetailViewModel : BaseViewModel
    {
        //public Book Book { get; set; }

        Book book;
        public Book Book
        {
            get { return book; }
            set { SetProperty(ref book, value); }
        }


        public BookDetailViewModel(Book book = null)
        {
            Book = book;

            //MessagingCenter.Subscribe<AddBook, Book>(this, "AddOrUpdateBook", async (obj, bookUpdate) =>
            //{
            //    if (bookUpdate != null)
            //    {

            //        Book = bookUpdate as Book;

            //        //Books.Add(_book);

            //        //await App.Database.SaveBookAsync(_book);
            //    }
            //});
        }
    }
}
