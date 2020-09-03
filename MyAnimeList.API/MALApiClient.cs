using MyAnimeList.API.DTOs;
using MyAnimeList.API.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyAnimeList.API
{
    /// <summary>
    /// Simple client to interact with MAL's API, requests are asyncronous.
    /// </summary>
    public class MALApiClient
    {
        private bool CanRenew;

        /// <summary>
        /// Gets a value indicating whether the client is currently authenticated,
        /// </summary>
        public bool IsAuthenticated { get; private set; }

        private UserParams UserParams;
        private HttpClient client;
        private OAuthToken Token;

        private string CodeVerifier;
        private string CodeChallenge;

        #region Constructors

        /// <summary>
        /// Creates a new instance of the API Client.
        /// </summary>
        /// <param name="Params">s asd asd asd</param>
        public MALApiClient(UserParams Params)
        {
            CanRenew = false;
            IsAuthenticated = false;
            UserParams = Params;
            client = new HttpClient();
            client.BaseAddress = new Uri(Consts.BaseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            CodeVerifier = CodeChallenge = GenerateCodeChallenge();
        }

        #endregion Constructors

        /// <summary>
        /// Returns a list of animes matching the provided string.
        /// </summary>
        /// <returns>A <see cref="GetAnimeListResult"/> containing the list of Animes, with their Name, Id and picture URLs</returns>
        public async Task<GetAnimeListResult> GetAnimeList(GetAnimeListRequest req)
        {
            GetAnimeListResult result;

            HttpResponseMessage response = await client.GetAsync(req.FormURL());
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<GetAnimeListResult>();//.ReadAsAsync<string>();
            }
            else
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new MALAPIException("Invalid Parameters", response);
                        break;

                    case HttpStatusCode.Unauthorized:
                        IsAuthenticated = false;
                        IEnumerable<string> values;
                        if (response.Headers.TryGetValues("WWW-Authenticate", out values))
                        {
                            string hdr = values.First();
                            if (hdr.Contains("exprired"))
                            {
                                if (CanRenew) RenewAuth();
                                else throw new MALAPIException("Unauthorized. Couldn't refresh the token automatically.", response);
                            }
                        }
                        throw new MALAPIException("Unauthorized. Couldn't refresh the token automatically.", response);
                        break;

                    default:
                        throw new MALAPIException(response);
                }
            }
            return result;
        }

        #region Authentication

        public string GetAuthUrl()
        {
            var nvc = new Dictionary<string, string>();
            nvc.Add("response_type", "code");
            nvc.Add("client_id", UserParams.ClientId);
            nvc.Add("code_challenge", CodeChallenge);
            nvc.Add("state", UserParams.OAuth2State);
            nvc.Add("redirect_uri", UserParams.RedirectURI);
            StringBuilder q = new StringBuilder();
            foreach (var x in nvc)
            {
                q.Append(x.Key + "=" + x.Value);
                if (!x.Equals(nvc.Last())) q.Append("&");
            }

            return Consts.AuthToken + "authorize?" + q;
        }

        public void DoAuth(string code)
        {
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("client_id", UserParams.ClientId));
            nvc.Add(new KeyValuePair<string, string>("code", code));
            nvc.Add(new KeyValuePair<string, string>("code_verifier", CodeVerifier));
            nvc.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            nvc.Add(new KeyValuePair<string, string>("redirect_uri", UserParams.RedirectURI));

            var req = new HttpRequestMessage(HttpMethod.Post, Consts.AuthToken + "token") { Content = new FormUrlEncodedContent(nvc) };

            ReadAuthResults(client.SendAsync(req).Result);
        }

        public void RenewAuth()
        {
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
            nvc.Add(new KeyValuePair<string, string>("refresh_token", Token.refresh_token));

            var req = new HttpRequestMessage(HttpMethod.Post, Consts.AuthToken + "token") { Content = new FormUrlEncodedContent(nvc) };

            ReadAuthResults(client.SendAsync(req).Result);
        }

        #endregion Authentication

        /*
    private static async Task<Product> UpdateProductAsync(Product product)
    {
        HttpResponseMessage response = await client.PutAsJsonAsync(
            $"api/products/{product.Id}", product);
        response.EnsureSuccessStatusCode();

        // Deserialize the updated product from the response body.
        product = await response.Content.ReadAsAsync<Product>();
        return product;
    }*/

        #region Private Methods

        private static string GenerateCodeChallenge()
        {
            Random random = new Random();
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789-._~";
            return new string(Enumerable.Repeat(chars, 128)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void ReadAuthResults(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var xd = response.Content.ReadAsStringAsync().Result;
                Token = response.Content.ReadAsAsync<OAuthToken>().Result;//.ReadAsAsync<string>();
                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", Token.access_token));
                IsAuthenticated = true;
                CanRenew = true;
            }
        }

        #endregion Private Methods
    }
}