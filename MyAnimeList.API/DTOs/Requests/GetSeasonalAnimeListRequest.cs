using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAnimeList.API.DTOs.Requests
{
    public class GetSeasonalAnimeListRequest : IURLRequestParams
    {
        /// <summary>
        /// Gets or sets the year, defaults to the current year
        /// </summary>
        public int Year { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// Gets or sets the desired season
        /// </summary>
        public Seasons Season { get; set; } = Seasons.WINTER;

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

        /// <summary>
        /// Gets or sets the sorting field. Defaults to <see cref="SortBy.ANIME_SCORE"/>
        /// </summary>
        public SortBy Sort { get; set; } = SortBy.ANIME_SCORE;

        public string FormURL()
        {
            string result = Consts.SeasonalAnimeList + String.Format($"{Year}/{Season.Value()}?limit ={Limit}&offset={Offset}&sort={Sort.Value()}");
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