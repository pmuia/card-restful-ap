using Core.Domain.Common;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Core.Domain.Utils
{
    public class Util
    {
        public static async Task<Tuple<HttpStatusCode, string>> AuthAsync(LimitedPool<HttpClient> httpClientPool, string payload, string url)
        {
            using (var httpClientContainer = httpClientPool.Get())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

                httpClientContainer.Value.DefaultRequestHeaders.Clear();

                var response = await httpClientContainer.Value.PostAsync(url, httpContent);

                var content = await response.Content.ReadAsStringAsync();

                return new Tuple<HttpStatusCode, string>(response.StatusCode, content);
            }
        }

        public static async Task<Tuple<HttpStatusCode, string>> GetAsync(LimitedPool<HttpClient> httpClientPool, string thirdPartyTransactionId, string url, string accessToken)
        {
            using (var httpClientContainer = httpClientPool.Get())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                httpClientContainer.Value.DefaultRequestHeaders.Clear();

                httpClientContainer.Value.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = await httpClientContainer.Value.GetAsync($"{url}{thirdPartyTransactionId}");

                string content = await response.Content.ReadAsStringAsync();

                return new Tuple<HttpStatusCode, string>(response.StatusCode, content);
            }
        }

        public static async Task<Tuple<HttpStatusCode, string>> PostAsync(LimitedPool<HttpClient> httpClientPool, string url, string payload, string accessToken)
        {
            using (var httpClientContainer = httpClientPool.Get())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 |
                                                       SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

                httpClientContainer.Value.DefaultRequestHeaders.Clear();

                httpClientContainer.Value.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = await httpClientContainer.Value.PostAsync(url, httpContent);

                var content = await response.Content.ReadAsStringAsync();

                return new Tuple<HttpStatusCode, string>(response.StatusCode, content);
            }
        }

        public static async Task<Tuple<HttpStatusCode, string>> PostUjumbeSMSAsync(LimitedPool<HttpClient> httpClientPool, string url, string payload, string authorization, string email)
        {
            using (var httpClientContainer = httpClientPool.Get())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 |
                                                       SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

                httpClientContainer.Value.DefaultRequestHeaders.Clear();

                httpClientContainer.Value.DefaultRequestHeaders.Add("X-Authorization", authorization);

                httpClientContainer.Value.DefaultRequestHeaders.Add("Email", email);

                var response = await httpClientContainer.Value.PostAsync(url, httpContent);

                var content = await response.Content.ReadAsStringAsync();

                return new Tuple<HttpStatusCode, string>(response.StatusCode, content);
            }
        }

        private static HttpWebResponse Post(byte[] buffer, string url, string accessToken)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var webRequest = WebRequest.Create(url) as HttpWebRequest;

            if (webRequest == null) throw new NullReferenceException("request is not a HTTP request");

            webRequest.Method = "POST";

            webRequest.ContentType = "application/json";

            webRequest.KeepAlive = true;

            //Auth2.0 verification. "Bearer" keyword followed by a space and generated Access Token from OAuth API.Like "Bearer xxxxxxx"
            webRequest.Headers.Add("Authorization", $"Bearer {accessToken}");

            using (Stream requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(buffer, 0, buffer.Length);

                requestStream.Close();
            }

            return webRequest.GetResponse() as HttpWebResponse;
        }

        private static AccessToken GetAccessToken(string consumerKey, string consumerSecret, string baseURL)
        {
            var result = new AccessToken();

            var resourceURL = $"{baseURL}?{"grant_type=client_credentials"}";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var webRequest = WebRequest.Create(resourceURL) as HttpWebRequest;

            if (webRequest == null)
                throw new NullReferenceException("request is not a http request");

            webRequest.Method = "GET";

            webRequest.KeepAlive = false;

            var basicAuth =
                "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(consumerKey + ":" + consumerSecret));

            webRequest.Headers.Add("Authorization", basicAuth);

            var webResponse = webRequest.GetResponse() as HttpWebResponse;

            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                using (var receiveStream = webResponse.GetResponseStream())
                {
                    using (var readStream = new StreamReader(receiveStream, new UTF8Encoding()))
                    {
                        string jsonResponse = readStream.ReadToEnd();

                        result = JsonConvert.DeserializeObject<AccessToken>(jsonResponse);
                    }
                }

                webResponse.Close(); //always close the stream
            }

            return result;
        }

    }
}
