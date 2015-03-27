using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OwinTokenAuthenticationClient
{
    public class ValuesClient
    {
        readonly string _accessToken;
        readonly Uri _baseRequestUri;
        public ValuesClient(Uri baseUri, string accessToken)
        {
            _accessToken = accessToken;
            _baseRequestUri = new Uri(baseUri, "api/values/");
        }


        // Handy helper method to set the access token for each request:
        void SetClientAuthentication(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", _accessToken);
        }


        public async Task<IEnumerable<string>> GetValuesAsync()
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);
                response = await client.GetAsync(_baseRequestUri);
            }
            var data = await response.Content.ReadAsStringAsync();
            var deserializedData = JsonConvert.DeserializeObject<IEnumerable<string>>(data);
            return deserializedData;
        }


        public async Task<string> GetValueAsync(int id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);

                // Combine base address URI and ID to new URI
                // that looks like http://hosturl/api/values/id
                response = await client.GetAsync(
                    new Uri(_baseRequestUri, id.ToString()));
            }
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
