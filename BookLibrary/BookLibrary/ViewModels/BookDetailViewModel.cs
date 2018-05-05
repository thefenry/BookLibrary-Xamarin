using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.ViewModels
{
    public class BookDetailViewModel
    {
        public Book Book { get; set; }
        public BookDetailViewModel(Book book = null)
        { 
            Book = book;
        }
    }
}
