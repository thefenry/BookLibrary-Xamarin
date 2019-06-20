using Newtonsoft.Json;
using SQLite;

namespace BookLibrary.Models
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }
        
        public string Description { get; set; }

        [JsonProperty("Series Title")]
        public string Series { get; set; }

        public string Genre { get; set; }

        public string Category { get; set; }

        public bool IsEBook { get; set; }
    }
}
