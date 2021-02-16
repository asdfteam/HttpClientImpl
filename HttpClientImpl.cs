using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientImpl
{
    public class HttpClientImpl
    {
        public HttpClient Client { get; }
        public HttpClientImpl(HttpClient client)
        {
            Client = client;
        }

        public async Task<HttpResponseMessage> Get(string uri, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            HandleHeaders(request, headers);

            var response = await Client.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Put(string uri, string body, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri) {Content = new StringContent(body)};
            
            HandleHeaders(request, headers);

            var response = await Client.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Post(string uri, string body, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri) {Content = new StringContent(body)};
            
            HandleHeaders(request, headers);

            var response = await Client.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Delete(string uri, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            HandleHeaders(request, headers);

            var response = await Client.SendAsync(request);
            return response;
        }

        private static void HandleHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
        {
            if (headers == null || headers.Count == 0) return;
            foreach (var keyValuePair in headers)
            {
                request.Headers.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}