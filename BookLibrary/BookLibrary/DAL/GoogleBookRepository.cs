using BookLibrary.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BookLibrary.DAL
{
    public class GoogleBookRepository
    {
        public async Task<BookResult> GetBookByISBNAsync(string isbn)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = $"https://www.googleapis.com/books/v1/volumes?q={isbn}+isbn&key=AIzaSyD6ZcI8kLpXhpMckhYWZoVsk0qjcM1PXWY";
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<BookResult>(dataObjects);

                    return result;

                }

                // TODO: Handle error

                return null;
            }
        }
    }
}
