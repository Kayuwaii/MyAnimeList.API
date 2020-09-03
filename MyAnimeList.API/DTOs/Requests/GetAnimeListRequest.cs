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

        /// <summary>
        /// Maximum amount of results returned. Defaults to 100. Maximum value 100.
        /// </summary>
        public int Limit { get; set; } = 100;

        /// <summary>
        /// The amount of results to skip. Defaults to 0.
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// An array containing the fields to be returned. Leave empty, for default fields.
        /// </summary>
        /// <remarks>
        /// To use this property you must create a class that can map the new properties and use it in //TODO: Custom method.
        /// </remarks>
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