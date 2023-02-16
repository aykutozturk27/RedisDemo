using Microsoft.Extensions.Configuration;

namespace RedisDemo.Core.Utilities.Configuration
{
    public static class CoreConfig
    {
        public static IConfigurationBuilder? _configurationBuilder;
        public static IConfiguration? _configuration;

        public static IConfiguration GetConfiguration()
        {
            _configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            _configuration= _configurationBuilder.Build();
            return _configuration;
        }

        public static IConfigurationSection GetValue(string key)
        {
            return GetConfiguration().GetSection(key);
        }

        public static string GetConnectionString(string connection)
        {
            return GetConfiguration().GetConnectionString(connection);
        }
    }
}
