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
            List<Book> books = await App.BookRepository.Get();

            return JsonConvert.SerializeObject(books, Formatting.Indented);
        }

        public async Task<int> ImportBooksAsync(byte[] dataArray)
        {
            string contents = System.Text.Encoding.UTF8.GetString(dataArray);
            List<Book> booksList = JsonConvert.DeserializeObject<List<Book>>(contents);

            foreach (var book in booksList)
            {
                await App.BookRepository.Insert(book);
            }

            MessagingCenter.Send(this, "Addbooks", booksList);

            return booksList.Count;
        }
    }
}
