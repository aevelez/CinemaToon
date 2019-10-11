using CinemaToon.Utilities.Abstractions.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CinemaToon.Utilities.Helpers
{
    public class ApiClient : IAPIClient
    {      
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }
        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
      
        public async Task<T> GetAsync<T>(Uri requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        public string Get(Uri requestUrl)
        {
            var response = _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead).Result;
            response.EnsureSuccessStatusCode();
            var data = response.Content.ReadAsStringAsync().Result;
            return data;
        }

        public async Task<T> GetAsyncWithUnderscorePropertyNames<T>(Uri requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings() { ContractResolver = new UnderscorePropertyNamesContractResolver() });
        }


        /// <summary>  
        /// Common method for making GET calls  
        /// </summary>  
        public async Task<T> GetAsync<T>(Uri requestUrl, string tokenName)
        {
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();          
            var token = JObject.Parse(data).SelectToken(tokenName);
            return JsonConvert.DeserializeObject<T>(token.ToString(), new JsonSerializerSettings() { ContractResolver = new UnderscorePropertyNamesContractResolver() });
        }
      
        public async Task<T> PostAsync<T>(string requestUrl, object dataToSend)
        {
            var json = JsonConvert.SerializeObject(dataToSend);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.PostAsync(requestUrl, byteContent);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
    }


    public class UnderscorePropertyNamesContractResolver : DefaultContractResolver
    {
        public UnderscorePropertyNamesContractResolver() : base()
        {
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return Regex.Replace(propertyName, @"(\w)([A-Z])", "$1_$2").ToLower();
        }
    }

}
