using MyAnimeList.API.DTOs;
using MyAnimeList.API.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private UserParams UserParams;
        private HttpClient client;
        private OAuthToken Token;

        private string CodeVerifier;
        private string CodeChallenge;

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
            else throw new MALAPIException(response);
            return result;
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

            HttpResponseMessage response = client.SendAsync(req).Result;
            if (response.IsSuccessStatusCode)
            {
                var xd = response.Content.ReadAsStringAsync().Result;
                Token = response.Content.ReadAsAsync<OAuthToken>().Result;//.ReadAsAsync<string>();
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", Token.access_token));
            }
        }

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

        public MALApiClient(UserParams Params)
        {
            UserParams = Params;
            client = new HttpClient();
            client.BaseAddress = new Uri(Consts.BaseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            CodeVerifier = CodeChallenge = GenerateCodeChallenge();
        }

        private static string GenerateCodeChallenge()
        {
            Random random = new Random();
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789-._~";
            return new string(Enumerable.Repeat(chars, 128)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}