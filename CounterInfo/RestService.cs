using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using CounterInfo.Shared;

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

            Uri uri = new Uri("https://localhost:7083/api/v1/counter_info/called");
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

        public async Task<States> SearchAsync(int id)
        {
            States Items;

            Uri uri = new Uri($"https://localhost:7083/api/v1/counter_info/{id}");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<States>(content, _serializerOptions);
                }
                else
                {
                    Items = States.Error;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Items = States.Error;
            }

            return Items;
        }
        //internal enum States
        //{
        //    /// <summary>待機中</summary>
        //    Waiting,
        //    /// <summary>未発券</summary>
        //    UnIssue,
        //    /// <summary>発券済</summary>
        //    Issue,
        //    /// <summary>呼出済</summary>
        //    Recept,
        //    /// <summary>対応済</summary>
        //    Called,
        //    /// <summary>エラー</summary>
        //    Error
        //}
    }
}
