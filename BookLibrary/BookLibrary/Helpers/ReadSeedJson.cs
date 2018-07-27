
using BookLibrary.Models;
using BookLibrary.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BookLibrary.Helpers
{
    public class ReadSeedJson
    {
        #region How to load an Json file embedded resource
        public static List<Book> GetSeedData()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(BooksViewModel)).Assembly;

            Stream stream = assembly.GetManifestResourceStream("BookLibrary.DAL.SeedData.json");

            List<Book> books;

            string json;
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
                books = JsonConvert.DeserializeObject<List<Book>>(json);
            }

            return books;
        }
        #endregion

    }
}
