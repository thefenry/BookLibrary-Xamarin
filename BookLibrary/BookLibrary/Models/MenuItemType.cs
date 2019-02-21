namespace BookLibrary.Models
{
    public enum MenuItemType
    {
        Browse,
        ImportExport
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }

}
