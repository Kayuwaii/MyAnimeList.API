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
        /// <summary>
        /// Returns a given <see cref="HttpContent"/>content as
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task<T> ReadAsAsync<T>(this HttpContent content) =>
             JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());

        public static string Value(this Seasons val) => Enum.GetNames(typeof(Seasons))[(int)val].ToLower();

        public static string Value(this SortBy val) => Enum.GetNames(typeof(SortBy))[(int)val].ToLower();
    }
}