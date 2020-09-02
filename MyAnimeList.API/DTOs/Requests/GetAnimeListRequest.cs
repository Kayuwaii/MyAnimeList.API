using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimeList.API.DTOs.Requests
{
    public class GetAnimeListRequest : IURLRequestParams
    {
        /// <summary>
        /// The string to search with. Must be 3 characters or longer.
        /// </summary>
        public string Search { get; set; } = String.Empty;

        public int Limit { get; set; } = 100;

        public int Offset { get; set; } = 0;
        public string[] Fields { get; set; } = { };

        public string FormURL()
        {
            string result = Consts.AnimeList + String.Format($"?q={Search}&limit={Limit}&offset={Offset}");
            if (Fields.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var x in Fields)
                {
                    sb.Append(x);
                    if (x != Fields.Last()) sb.Append(",");
                }
                result += "&fields=" + sb.ToString();
            }
            return result;
        }
    }
}