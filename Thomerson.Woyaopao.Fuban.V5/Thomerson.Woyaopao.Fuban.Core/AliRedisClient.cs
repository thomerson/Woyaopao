using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Fuban.Core
{
    public class AliRedisClient
    {
        // redis config
        private static ConfigurationOptions configurationOptions = ConfigurationOptions.Parse("r-uf64b06de44c1e44.redis.rds.aliyuncs.com:6379,password=Woyaopao2018,connectTimeout=2000");

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
