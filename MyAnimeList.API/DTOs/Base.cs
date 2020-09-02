using System;
using System.Collections.Generic;
using System.Text;

namespace MyAnimeList.API.DTOs
{
    public abstract class Base<T1>
    {
        public IEnumerable<T1> data { get; set; }
        public Paging paging { get; set; }
    }
}