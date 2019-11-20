using BookLibrary.Models;
using Newtonsoft.Json;
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
            var exportObject = new ImportExportObject
            {
                Books = await App.BookRepository.Get(),
                Movies = await App.MoviesRepository.Get()
            };

            return JsonConvert.SerializeObject(exportObject, Formatting.Indented);
        }

        public async Task<int> ImportBooksAsync(byte[] dataArray)
        {
            string contents = System.Text.Encoding.UTF8.GetString(dataArray);
            ImportExportObject importObject = JsonConvert.DeserializeObject<ImportExportObject>(contents);

            foreach (var book in importObject.Books)
            {
                await App.BookRepository.Insert(book);
            }

            foreach (var movie in importObject.Movies)
            {
                await App.MoviesRepository.Insert(movie);
            }

            MessagingCenter.Send(this, "Addbooks", importObject.Books);

            return importObject.Books.Count;
        }
    }
}
