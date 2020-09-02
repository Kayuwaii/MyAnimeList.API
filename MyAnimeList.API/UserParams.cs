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

        /// <summary>
        /// This is the URI the user will be redirected after allowing the API usage, must mutch one of the URLs you provided when applying for an API key. By default it points http://localhost. But it's recomended to change this to a web service that will process the URL.
        /// </summary>
        public string RedirectURI { get; set; }
    }
}