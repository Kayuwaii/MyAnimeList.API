using System;
using System.Collections.Generic;
using System.Text;

namespace MyAnimeList.API
{
    internal class Consts
    {
        public const string BaseURL = "https://api.myanimelist.net/v2/";
        public const string AuthToken = "https://myanimelist.net/v1/oauth2/";
        public const string AnimeList = "anime";
        public const string SeasonalAnimeList = "anime/season/";
    }

    public enum Seasons
    {
        WINTER,
        SPRING,
        SUMMER,
        FALL
    }

    public enum SortBy
    {
        ANIME_SCORE,
        ANIME_NUM_LIST_USERS
    }
}