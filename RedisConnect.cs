using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace KeyValue
{
    public class RedisConnect
    {
        private static ConnectionMultiplexer myRedis;

        public static  ConnectionMultiplexer  Redis
        {
            get { return myRedis; }
            set { myRedis = value; }
        }


        public static ConfigurationOptions config;
        public static bool Connect()
        {
            Redis = ConnectionMultiplexer.Connect(config);
            return true;
        }


    }
}







       
    
