using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyAnimeList.API
{
    public static class Extensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content) =>
             JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());

        public static string Value(this Seasons val) => Enum.GetNames(typeof(Seasons))[(int)val].ToLower();

        public static string Value(this SortBy val) => Enum.GetNames(typeof(SortBy))[(int)val].ToLower();
    }
}