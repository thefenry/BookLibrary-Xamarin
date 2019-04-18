using SQLite;

namespace BookLibrary.Models
{
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public string MovieType { get; set; }

        public int Year { get; internal set; }
    }
}
