using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CounterInfo
{
    internal class RestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        //public List<TodoItem> Items { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<int>> RefreshDataAsync()
        {
            var Items = new List<int>();

            Uri uri = new Uri("https://localhost:7083/api/called");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<int>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }
    }
}
