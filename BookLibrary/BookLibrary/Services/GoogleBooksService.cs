using BookLibrary.DAL;
using BookLibrary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Services
{
    public class GoogleBooksService
    {
        GoogleBookRepository googleBookRepository = new GoogleBookRepository();
        public async Task<Book> GetBookAsync(string isbn)
        {
            BookResult bookResult = await googleBookRepository.GetBookByISBNAsync(isbn);

            if (bookResult?.Items?.Count > 0)
            {
                Item book = bookResult.Items.First();

                return new Book
                {
                    Author = SetAuthor(book),

                    Title = SetTitle(book),

                    Description = SetDescription(book),

                    IsEBook = SetIsEbook(book),

                    Category = SetCategory(book)
                };
            }

            return null;
        }

        private string SetAuthor(Item book)
        {
            if (book?.VolumeInfo?.Authors == null)
            {
                return null;
            }
            else
            {
                return string.Join(",", book.VolumeInfo.Authors);
            }
        }

        private string SetTitle(Item book)
        {
            if (book?.VolumeInfo?.Title == null)
            {
                return null;
            }
            else
            {
                return book.VolumeInfo.Title;
            }
        }

        private string SetDescription(Item book)
        {
            if (book?.VolumeInfo?.Description == null)
            {
                return null;
            }
            else
            {
                return book.VolumeInfo.Description;
            }
        }

        private bool SetIsEbook(Item book)
        {
            if (book.SaleInfo == null)
            {
                return false;
            }
            else
            {
                return book.SaleInfo.IsEbook;
            }
        }

        private string SetCategory(Item book)
        {
            if (book?.VolumeInfo?.Categories == null)
            {
                return null;
            }
            else
            {
                return string.Join(",", book.VolumeInfo.Categories);
            }
        }

        //public void Dispose()
        //{
        //    Dispose(true);            
        //    GC.SuppressFinalize(this);
        //}

        //private bool isDisposed;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (isDisposed)
        //    {
        //        return;
        //    }

        //    if (disposing)
        //    {
        //        //googleBookRepository.Dispose();
        //    }

        //    isDisposed = true;
        //}
    }
}
