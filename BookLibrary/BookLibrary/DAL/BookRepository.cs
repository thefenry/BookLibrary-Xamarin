using BookLibrary.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibrary.DAL
{
    public class BookRepository
    {
        readonly SQLiteAsyncConnection database;

        public BookRepository(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Book>().Wait();
        }

        public Task<List<Book>> GetBooksAsync()
        {
            return database.Table<Book>().ToListAsync();
        }

        public Task<List<Book>> GetBooksById(int id)
        {
            return database.QueryAsync<Book>($"SELECT * FROM [Books] WHERE [Id] = {id}");
        }

        public Task<Book> GetBookAsync(int id)
        {
            return database.Table<Book>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveBookAsync(Book book)
        {
            if (book.Id != 0)
            {
                return database.UpdateAsync(book);
            }
            else
            {
                return database.InsertAsync(book);
            }
        }

        public async Task<List<Book>> SearchBooksAsync(string searchText)
        {
            return await database.Table<Book>().Where(x => x.Title.ToLower().Contains(searchText.ToLower()) 
            || x.Author.ToLower().Contains(searchText.ToLower())).ToListAsync();
        }

        public Task<int> DeleteBookAsync(Book book)
        {
            return database.DeleteAsync(book);
        }
    }
}
