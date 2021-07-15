using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.IO
{
    public class JsonDataReader : IDataReader
    {
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        private readonly string _filePath;

        public JsonDataReader(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Convert a JSON array of line items into a list of line item objects
        /// </summary>
        /// <returns>A deserialized list of line items or an empty list if there was an error</returns>
        public async Task<IList<LineItem>> ReadDataAsync()
        {
            try
            {
                using FileStream openStream = File.OpenRead(_filePath);
                return await JsonSerializer.DeserializeAsync<IList<LineItem>>(openStream, options);
            }
            catch (Exception)    // something went wrong reading or parsing, return an empty list and warn the user
            {
                Console.WriteLine($"{_filePath} could not be read or parsed.");
                return new List<LineItem>();
            }
        }
    }
}
