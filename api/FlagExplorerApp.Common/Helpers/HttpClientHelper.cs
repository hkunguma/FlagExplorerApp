using FlagExplorerApp.Common.Contracts;
using System;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlagExplorerApp.Common.Helpers
{
    public class HttpClientHelper<TEntity> : IHttpClient<TEntity> 
    {
        private readonly string _baseAddress;

        private readonly string? _authScheme;
        private readonly string? _authToken;

        public HttpClientHelper(string baseAddress, string? authScheme = null, string? authToken = null)
        {
            _baseAddress = baseAddress;
            _authScheme = authScheme;
            _authToken = authToken;
        }

        public async Task<TEntity?> GetAsync(string requestUri)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                using (HttpClient client = Client())
                {
                    httpResponse = await client.GetAsync(requestUri);

                    if (httpResponse == null)
                        return default(TEntity);

                    using (HttpContent content = httpResponse.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        var options = new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };

                        var response = JsonSerializer.Deserialize<TEntity>(result, options);

                        return response;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                //replace logger
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                //replace logger
                Console.WriteLine(ex.Message);
            }

            return default(TEntity);
        }

        private HttpClient Client()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(_authScheme))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_authScheme, _authToken);

            return httpClient;
        }
    }
}
