using Identity.Package.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Package.Rules;

public interface IApiCall : IAsyncDisposable
{
    Task<string> PostAsync<TValue>(string url,TValue value);

    Task<string> GetAsync(string url);
}