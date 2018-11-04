using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Core
{
    public class AliRedisClient
    {
        // redis config
        private static string RedisUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisUrl"];
            }
        }
        private static string RedisPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisPassword"];
            }
        }

        private static string configOption
        {
            get
            {
                return string.Format("{0},password={1},connectTimeout=2000", RedisUrl, RedisPassword);

            }
        }

        private static ConfigurationOptions configurationOptions = ConfigurationOptions.Parse(configOption);

        //the lock for singleton
        private static readonly object Locker = new object();

        //singleton
        private static ConnectionMultiplexer redisConn;

        private static IDatabase db;

        //singleton
        public static ConnectionMultiplexer getRedisConn()
        {

            if (redisConn == null)
            {
                lock (Locker)
                {
                    if (redisConn == null || !redisConn.IsConnected)
                    {
                        redisConn = ConnectionMultiplexer.Connect(configurationOptions);
                    }
                }
            }
            return redisConn;
        }

    }
}
