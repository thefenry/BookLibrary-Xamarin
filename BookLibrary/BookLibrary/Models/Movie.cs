using SQLite;

namespace BookLibrary.Models
{
    [Table("Movies")]
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public string MovieType { get; set; }

        public int? Year
        {
            get
            {
                if (string.IsNullOrWhiteSpace(StringYear))
                {
                    return null;
                }

                bool parsed = int.TryParse(StringYear, out int year);
                if (!parsed)
                {
                    return null;
                }
                else
                {
                    return year;
                }

            }
            set
            {
                if (value.HasValue)
                {
                    StringYear = value.ToString();
                }
                else
                {
                    StringYear = null;
                }
            }
        }

        public string StringYear
        {
            get; set;
        }

        public bool IsSteelBook { get; set; }
    }
}
