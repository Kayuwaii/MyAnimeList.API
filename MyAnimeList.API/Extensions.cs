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
    }
}