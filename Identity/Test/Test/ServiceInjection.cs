using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servant.Service.Implemention.Injector;
using System;
using System.IO;

namespace Test.Injection;

public class ServiceInjection
{
    readonly IServiceCollection _serviceCollection;

    readonly IServiceProvider _serviceProvider;

    readonly IConfigurationRoot _configuration;

    public ServiceInjection()
    {
        _serviceCollection = new ServiceCollection();
        _serviceProvider = _serviceCollection.BuildServiceProvider();
        _configuration = CreateConfiguration();
        InjectServices();
    }

    public T? GetService<T>(Type serviceType)
    {
        var service = _serviceProvider.GetService(serviceType);
        if (service != null)
            return (T)service;

        return default(T);
    }

    public async void InjectServices()
    {
        await _serviceCollection.AddIdentityServicesAsync(_configuration);
    }

    public IConfigurationRoot CreateConfiguration()
    {
        var dir = Directory.GetCurrentDirectory();
        var settingPath = Directory.GetCurrentDirectory() + "/appsettings.json";

        if (!File.Exists(settingPath))
        {
            FileStream fs = new(settingPath, FileMode.OpenOrCreate);
            fs.Write(new byte[] { });
            fs.Close();
            fs.Dispose();
        }

        var config = new ConfigurationBuilder().SetBasePath(dir)
                .AddJsonFile(settingPath).Build();
        return config;
    }
}
