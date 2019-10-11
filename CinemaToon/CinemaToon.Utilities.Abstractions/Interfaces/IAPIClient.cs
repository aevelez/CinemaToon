using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaToon.Utilities.Abstractions.Interfaces
{
    public interface IAPIClient
    {
        string Get(Uri requestUrl);
        Task<T> GetAsync<T>(Uri requestUrl);
        Task<T> GetAsyncWithUnderscorePropertyNames<T>(Uri requestUrl);
        Task<T> GetAsync<T>(Uri requestUrl, string tokenName);
        Task<T> PostAsync<T>(string requestUrl, object dataToSend);
    }
}
