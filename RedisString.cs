using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace KeyValue
{
    public class RedisString
    {

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
