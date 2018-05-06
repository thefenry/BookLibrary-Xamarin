using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.Models
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; internal set; }

        public string Title { get; set; }

        public string Author { get; set; }
    }
}
