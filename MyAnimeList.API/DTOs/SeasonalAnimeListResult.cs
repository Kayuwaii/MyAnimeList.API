using System;
using System.Collections.Generic;
using System.Text;

namespace MyAnimeList.API.DTOs
{
    public class SeasonalAnimeListResult : AnimeListResult
    {
        public Season Season { get; set; }
    }
}