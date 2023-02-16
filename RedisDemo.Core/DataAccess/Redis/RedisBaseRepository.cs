using Microsoft.Extensions.Configuration;
using RedisDemo.Core.Utilities.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisDemo.Core.DataAccess.Redis
{
    public class RedisBaseRepository : IRedisBaseRepository
    {
        private ConfigurationOptions configurationOptions;
        private IConfiguration _configuration;

        public RedisBaseRepository()
        {
            _configuration = CoreConfig.GetConfiguration();
            configurationOptions = new ConfigurationOptions
            {
                EndPoints = { $"{_configuration.GetValue<string>("Redis:Host")}:{_configuration.GetValue<int>("Redis:Port")}" },
                User = $"{_configuration.GetValue<string>("Redis:User")}",
                Password = $"{_configuration.GetValue<string>("Redis:Password")}",
                Ssl = _configuration.GetValue<bool>("Redis:Ssl"),
                AbortOnConnectFail = false,
            };
        }

        public T? Get<T>(string key, int db = 0)
        {
            using (var connection = ConnectionMultiplexer.Connect(configurationOptions))
            {
                IDatabase database = connection.GetDatabase(db);
                if (Any(key, db))
                {
                    string jsonData = database.StringGet(key);
                    var data = JsonSerializer.Deserialize<T>(jsonData);
                    return data;
                }
            }
            return default;
        }

        public void Set(string key, object data, double duration = 0, int db = 0)
        {
            using (var connection = ConnectionMultiplexer.Connect(configurationOptions))
            {
                IDatabase database = connection.GetDatabase(db);

                string jsonData = JsonSerializer.Serialize(data);
                database.StringSet(key, jsonData, TimeSpan.FromMinutes((double)duration));
            }
        }

        public void Set<T>(string key, T data, double duration = 0, int db = 0) where T : class
        {
            using (var connection = ConnectionMultiplexer.Connect(configurationOptions))
            {
                IDatabase database = connection.GetDatabase(db);

                string jsonData = JsonSerializer.Serialize(data);
                database.StringSet(key, jsonData, TimeSpan.FromMinutes((double)duration));
            }
        }

        public void Set(KeyValuePair<RedisKey, RedisValue>[] keyValue, double duration = 0, int db = 0)
        {
            //using (var connecion = ConnectionMultiplexer.Connect(configurationOptions))
            //{
            //IDatabase database = connecion.GetDatabase(db);
            //var batch = database.CreateBatch();
            //batch.StringSetAsync(keyValue);
            //keyValue.ToList().ForEach(kv =>
            //{
            //    batch.KeyExpireAsync(kv.Key, TimeSpan.FromMinutes((double)duration));
            //});
            //batch.Execute();

            //const int BatchSize = 10000;
            //var list = new List<KeyValuePair<RedisKey, RedisValue>>(BatchSize);
            //foreach (var pair in keyValue)
            //{
            //    list.Add(new KeyValuePair<RedisKey, RedisValue>(pair.Key, pair.Value));
            //    if (list.Count == BatchSize)
            //    {
            //        using (var _connection = ConnectionMultiplexer.Connect(configurationOptions))
            //        {
            //            IDatabase database = _connection.GetDatabase(db);
            //            var _batch = database.CreateBatch();
            //            _batch.StringSetAsync(list.ToArray());
            //            list.ForEach(kv =>
            //            {
            //                _batch.KeyExpireAsync(kv.Key, TimeSpan.FromMinutes((double)duration));
            //            });
            //            _batch.Execute();
            //            list.Clear();
            //        }
            //    }
            //}

            //if (list.Count != 0)
            //{
            //    using (var _connection = ConnectionMultiplexer.Connect(configurationOptions))
            //    {
            //        IDatabase database = _connection.GetDatabase(db);
            //        var _batch = database.CreateBatch();
            //        _batch.StringSetAsync(list.ToArray());
            //        list.ForEach(kv =>
            //        {
            //            _batch.KeyExpireAsync(kv.Key, TimeSpan.FromMinutes((double)duration));
            //        });
            //        _batch.Execute();
            //    }
            //}

            //const int BatchSize = 50;
            //var batch = new List<KeyValuePair<RedisKey, RedisValue>>(BatchSize);
            //foreach (var pair in keyValue)
            //{
            //    batch.Add(new KeyValuePair<RedisKey, RedisValue>(pair.Key, pair.Value));

            //    if (batch.Count == BatchSize)
            //    {
            //        database.StringSet(batch.ToArray());
            //        batch.Clear();
            //    }
            //}

            //if (batch.Count != 0)
            //{
            //    database.StringSet(batch.ToArray());
            //}
            //}

            //const int BatchSize = 5000;
            //var batch = new List<KeyValuePair<RedisKey, RedisValue>>(BatchSize);
            //foreach (var pair in keyValue)
            //{
            //    batch.Add(new KeyValuePair<RedisKey, RedisValue>(pair.Key, pair.Value));

            //    if (batch.Count == BatchSize)
            //    {
            //        using var _connection = ConnectionMultiplexer.Connect(configurationOptions);
            //        var _cache = _connection.GetDatabase(db);
            //        _cache.StringSet(batch.ToArray());
            //        batch.Clear();
            //    }
            //}

            //if (batch.Count != 0)
            //{
            //    using var _connection = ConnectionMultiplexer.Connect(configurationOptions);
            //    var _cache = _connection.GetDatabase(db);
            //    _cache.StringSet(batch.ToArray());
            //}
        }

        public bool Any(string key, int db = 0)
        {
            using (var connection = ConnectionMultiplexer.Connect(configurationOptions))
            {
                IDatabase database = connection.GetDatabase(db);
                return database.KeyExists(key);
            }
        }

        public void Remove(string key, int db = 0)
        {
            using (var connection = ConnectionMultiplexer.Connect(configurationOptions))
            {
                IDatabase database = connection.GetDatabase(db);
                database.KeyDelete(key);
            }
        }

        public void Remove(RedisKey[] key, int db = 0)
        {
            using (var connection = ConnectionMultiplexer.Connect(configurationOptions))
            {
                IDatabase database = connection.GetDatabase(db);
                database.KeyDelete(key);
            }
        }
    }
}
