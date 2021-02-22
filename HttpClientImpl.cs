using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientImpl
{
    public interface IHttpClientImpl
    { 
        Task<HttpResponseMessage> Get(string uri, Dictionary<string, string> headers = null);
        Task<HttpResponseMessage> Put(string uri, string body = null, Dictionary<string, string> headers = null);
        Task<HttpResponseMessage> Post(string uri, string body, Dictionary<string, string> headers = null);
        Task<HttpResponseMessage> Delete(string uri, Dictionary<string, string> headers = null);

        void HandleHeaders(HttpRequestMessage request, Dictionary<string, string> headers);

    }

    public class HttpClientImpl : IHttpClientImpl
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

        public async Task<HttpResponseMessage> Put(string uri, string body = null, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StringContent(body ?? string.Empty)
            };
            
            HandleHeaders(request, headers);

            var response = await Client.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> Post(string uri, string body, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri) {Content = new StringContent(body ?? string.Empty)};
            
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

        public void HandleHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
        {
            if (request.Method.Equals(HttpMethod.Post))
            {
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            if (headers == null || headers.Count == 0) return;
            foreach (var keyValuePair in headers)
            {
                request.Headers.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}