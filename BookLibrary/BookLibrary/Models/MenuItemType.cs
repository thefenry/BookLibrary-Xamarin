namespace BookLibrary.Models
{
    public enum MenuItemType
    {
        Books,
        Movies,
        ImportExport
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }

}
