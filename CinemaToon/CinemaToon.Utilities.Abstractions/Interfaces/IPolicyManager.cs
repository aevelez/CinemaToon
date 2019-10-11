using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CinemaToon.Entities.Movies;

namespace CinemaToon.Utilities.Abstractions.Interfaces
{
    public interface IPolicyManager
    {
        Task<T> GetFallbackPolicy<T>(Func<CancellationToken, Task<T>> fallBackAction, Func<Task<T>> action);
    }
}
