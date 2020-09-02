using System;
using System.Collections.Generic;
using System.Text;

namespace MyAnimeList.API.DTOs
{
    public class SeasonalAnimeListResult : GetAnimeListResult
    {
        public Season Season { get; set; }
    }
}