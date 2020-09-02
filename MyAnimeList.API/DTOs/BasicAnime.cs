using System;
using System.Collections.Generic;
using System.Text;

namespace MyAnimeList.API.DTOs
{
    public class BasicAnime
    {
        public int id { get; set; }
        public string title { get; set; }

        public PictureInfo main_picture { get; set; }
    }
}