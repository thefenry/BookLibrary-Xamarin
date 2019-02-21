using BookLibrary.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public async Task<int> ImportBooksAsync(byte[] dataArray)
        {
            string contents = System.Text.Encoding.UTF8.GetString(dataArray);
            List<Book> booksList = JsonConvert.DeserializeObject<List<Book>>(contents);

            await App.Database.SaveBookBatchAsync(booksList);

            MessagingCenter.Send(this, "Addbooks", booksList);

            return booksList.Count;
        }
    }
}
