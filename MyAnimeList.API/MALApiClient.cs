using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyAnimeList.API
{
    public class MALApiClient
    {
        private UserParams UserParams;
        private HttpClient client;
        private OAuthToken Token;

        private string CodeVerifier;
        private string CodeChallenge;

        public async Task<string> GetAnimeList(int limit = 100)
        {
            string result = String.Empty;
            string uri = Consts.AnimeList + "?limit=" + limit;
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();//.ReadAsAsync<string>();
            }
            return result;
        }

        public async void DoAuth(string code)
        {
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("client_id", UserParams.ClientId));
            nvc.Add(new KeyValuePair<string, string>("code", code));
            nvc.Add(new KeyValuePair<string, string>("code_verifier", CodeVerifier));
            nvc.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));

            var req = new HttpRequestMessage(HttpMethod.Post, Consts.AuthToken + "token") { Content = new FormUrlEncodedContent(nvc) };

            HttpResponseMessage response = await client.SendAsync(req);
            if (response.IsSuccessStatusCode)
            {
                var xd = await response.Content.ReadAsStringAsync();
                Token = await response.Content.ReadAsAsync<OAuthToken>();//.ReadAsAsync<string>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            }
        }

        public string GetAuthUrl()
        {
            var nvc = new Dictionary<string, string>();
            nvc.Add("response_type", "code");
            nvc.Add("client_id", UserParams.ClientId);
            nvc.Add("code_challenge", CodeChallenge);
            nvc.Add("state", UserParams.OAuth2State);
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