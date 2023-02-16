using Polly;
using Polly.Retry;
using StackExchange.Redis;

namespace RedisDemo.Core.DataAccess.Redis
{
    public class RedisConnectorHelper
    {
        //static RedisConnectorHelper()
        //{
        //    RedisConnectorHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        //    {
        //        return ConnectionMultiplexer.Connect("localhost:6379");
        //    });
        //}

        //private static Lazy<ConnectionMultiplexer> lazyConnection;

        //public static ConnectionMultiplexer Connection
        //{
        //    get
        //    {
        //        return lazyConnection.Value;
        //    }
        //}

        #region Connection
        private static Lazy<ConnectionMultiplexer> lazyConnection = CreateConnection();

        public static ConnectionMultiplexer Connection
        {
            get { return lazyConnection.Value; }
        }

        private static Lazy<ConnectionMultiplexer> CreateConnection()
        {
            return new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("localhost");//Provide your connecting string
            });
        }
        #endregion

        #region RetryPolicy
        private static RetryPolicy retryPolicy = Policy
                                                    .Handle<Exception>()
                                                    .WaitAndRetry(5, p =>
                                                    {
                                                        var timeToWait = TimeSpan.FromSeconds(90);
                                                        Console.WriteLine($"Waiting for reconnection {timeToWait}");
                                                        return timeToWait;
                                                    }); 
        #endregion

        //Get Redis Database
        public static IDatabase GetDatabase()
        {
            return retryPolicy.Execute(() => Connection.GetDatabase());
        }

        //Get Redis Endpoint
        public static System.Net.EndPoint[] GetEndPoints()
        {
            return retryPolicy.Execute(() => Connection.GetEndPoints());
        }

        //Get Redis Server
        public static IServer GetServer(string host, int port)
        {
            return retryPolicy.Execute(() => Connection.GetServer(host, port));
        }
    }
}
