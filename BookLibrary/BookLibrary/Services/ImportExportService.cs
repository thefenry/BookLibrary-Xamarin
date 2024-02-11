using System.Threading.Tasks;
using BookLibrary.Models;
using Newtonsoft.Json;
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
            ImportExportObject exportObject = new ImportExportObject
            {
                Books = await App.BookRepository.Get(),
                Movies = await App.MoviesRepository.Get()
            };

            return JsonConvert.SerializeObject(exportObject, Formatting.Indented);
        }

        public async Task<int> ImportBooksAsync(string contents)
        {
            ImportExportObject importObject = JsonConvert.DeserializeObject<ImportExportObject>(contents);

            foreach (Book book in importObject.Books)
            {
                await App.BookRepository.Insert(book);
            }

            foreach (Movie movie in importObject.Movies)
            {
                await App.MoviesRepository.Insert(movie);
            }

            MessagingCenter.Send(this, "Addbooks", importObject.Books);

            return importObject.Books.Count;
        }
    }
}
