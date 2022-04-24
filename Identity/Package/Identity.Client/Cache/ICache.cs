using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Client.Cache;

public interface ICache : IAsyncDisposable
{
    Task<TModel?> GetItemAsync<TModel>(string key);

    Task<TModel?> SetItemAsync<TModel>(string key, TModel obj);

    Task<ConnectionMultiplexer> ConnectAsync();
}
