using System;
using System.Collections.Generic;
using System.Text;

namespace MyAnimeList.API
{
    public class UserParams
    {
        /// <summary>
        /// The ClientId obtained from MyAnimeList.
        ///
        /// To register a new App and obtain a ClientId, visit <a href="https://myanimelist.net/apiconfig">MyAnimeList</a>
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// An opaque value used by the client to maintain state between the request and callback.
        /// </summary>
        public string OAuth2State { get; set; }
    }
}