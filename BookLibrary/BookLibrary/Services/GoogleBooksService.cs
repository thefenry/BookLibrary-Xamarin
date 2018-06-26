using BookLibrary.DAL;
using BookLibrary.Models;
using System.Threading.Tasks;

namespace BookLibrary.Services
{
    public class GoogleBooksService
    {
        public async Task<Book> GetBookAsync(string isbn)
        {

            GoogleBookRepository googleBookRepository = new GoogleBookRepository();

            BookResult bookResult = await googleBookRepository.GetBookByISBNAsync(isbn);
            if (bookResult == null)
            {
                return null;
            }

            return new Book
            {
                Author = string.Join(",", bookResult.Items[0].VolumeInfo.Authors),

                Title = bookResult.Items[0].VolumeInfo.Title,

                Description = bookResult.Items[0].VolumeInfo.Description,

                IsEBook = bookResult.Items[0].SalesInfo.IsEbook,

                Category = string.Join(",", bookResult.Items[0].VolumeInfo.Categories)
            };
        }
    }
}
