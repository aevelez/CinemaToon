using CinemaToon.Utilities.Abstractions.Interfaces;
using Polly;
using Polly.Extensions.Http;
using Polly.Fallback;
using Polly.Wrap;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaToon.Utilities.Helpers
{
    public class PolicyManager : IPolicyManager
    {
        private const int NUMBER_OF_RETRIES = 3;

        public Task<T> GetFallbackPolicy<T>(Func<CancellationToken, Task<T>> fallBackAction, Func<Task<T>> action)
        {
            return Policy<T>
                .Handle<Exception>()
                 .FallbackAsync(fallBackAction)
                 .ExecuteAsync(action);
        }

        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            Random jitterer = new Random();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrTransientHttpError()
                .OrTransientHttpStatusCode()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound
                || msg.StatusCode == HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(NUMBER_OF_RETRIES, retryAttempt =>
                                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                                + TimeSpan.FromMilliseconds(jitterer.Next(0, 100)));

        }

    }
}
