using Newtonsoft.Json;
using Cards.WEB.Services.Interface.Authentication;
using System.Net;
using System.Text;

namespace Cards.WEB.Services.Implementation.Authentication
{
    public class RequestService : IRequestService
    {
        public async Task<TResult> PostAsync<TRequest, TResult>(string uri, string payload, string token)
        {
            var httpClient = CreateHttpClient(GlobalSettings.Username, GlobalSettings.Password, token);

            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, httpContent);

            await HandleResponse(response);

            var responseData = await response.Content.ReadAsStringAsync();

            var deserializedResponse = JsonConvert.DeserializeObject<TResult>(responseData);

            return deserializedResponse;
        }

        public async Task<TResult> GetAsync<TRequest, TResult>(string uri, string token)
        {
            var httpClient = CreateHttpClient(GlobalSettings.Username, GlobalSettings.Password, token);

            //var httpContent = new StringContent(Encoding.UTF8, "application/json");

            var response = await httpClient.GetAsync(uri);

            await HandleResponse(response);

            var responseData = await response.Content.ReadAsStringAsync();

            var deserializedResponse = JsonConvert.DeserializeObject<TResult>(responseData);

            return deserializedResponse;
        }

        public async Task<TResult> PutAsync<TRequest, TResult>(string uri, string payload, string token)
        {
            var httpClient = CreateHttpClient(GlobalSettings.Username, GlobalSettings.Password, token);

            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(uri, httpContent);

            await HandleResponse(response);

            var responseData = await response.Content.ReadAsStringAsync();

            var deserializedResponse = JsonConvert.DeserializeObject<TResult>(responseData);

            return deserializedResponse;
        }

        HttpClient CreateHttpClient(string userName, string password, string token)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Clear();

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }

            return httpClient;
        }

        async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }
    }
}
