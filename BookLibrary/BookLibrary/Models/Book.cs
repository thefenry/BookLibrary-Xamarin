using Newtonsoft.Json;
using SQLite;

namespace BookLibrary.Models
{
    [Table("Books")]
    public class Book
    {
        /// <summary>
        /// DO NOT CHNAGE THIS!!!!!! THE DB DOES NOT LIKE IT
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } ///

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
