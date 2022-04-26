using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Identity.Client.Cache;

public class Cache : ICache
{
    readonly IConfiguration _configuration;

    public Cache(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<ConnectionMultiplexer> ConnectAsync()
    {
        var redisConnection = _configuration["Identity:Cache:Redis:Connection"];
        ConnectionMultiplexer redis = await ConnectionMultiplexer.ConnectAsync(redisConnection);
        return redis;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public async Task<TModel?> GetItemAsync<TModel>(string key)
    {
        var redis = await ConnectAsync();
        IDatabase? dataBase = redis.GetDatabase();
        RedisValue value = await dataBase.StringGetAsync(new RedisKey(key));
        if (value.HasValue)
        {
            TModel? model = JsonConvert.DeserializeObject<TModel>(value.ToString());
            return model;
        }
        return default;
    }

    public async Task<TModel?> SetItemAsync<TModel>(string key, TModel obj)
    {
        var redis = await ConnectAsync();
        IDatabase? dataBase = redis.GetDatabase();
        string? json = JsonConvert.SerializeObject(obj);
        if (await dataBase.StringSetAsync(new RedisKey(key), new RedisValue(json)))
            return obj;
        return default;
    }
}
