using System;
using System.Collections.Generic;
using System.Text;

namespace MyAnimeList.API
{
    internal class OAuthToken
    {
        public string token_type { get; set; }
        public long expires_in { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}