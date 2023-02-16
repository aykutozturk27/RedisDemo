using StackExchange.Redis;

namespace RedisDemo.Core.DataAccess.Redis
{
    public interface IRedisBaseRepository
    {
        T Get<T>(string key, int db = 0);
        void Set(string key, object data, double duration = 0, int db = 0);
        void Set<T>(string key, T data, double duration = 0, int db = 0) where T : class;
        void Set(KeyValuePair<RedisKey, RedisValue>[] keyValue, double duration = 0, int db = 0);
        void Remove(string key, int db = 0);
        void Remove(RedisKey[] key, int db = 0);
        bool Any(string key, int db = 0);
    }
}
