using BookLibrary.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibrary.Services
{
    public class ImportExportService
    {
        public ImportExportService()
        {
        }

        public async Task<string> GetExportLibraryContentAsync()
        {
            List<Book> books = await App.Database.GetBooksAsync();

            return JsonConvert.SerializeObject(books, Formatting.Indented);
        }
    }
}
