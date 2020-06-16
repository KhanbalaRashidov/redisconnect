using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyValue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Redis_Api_Connection.Controllers
{
    [Route("new-api/")]
    [ApiController]
    [Produces("application/json")]
    public class RedisControllerNew : ControllerBase
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisControllerNew(ConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        [HttpGet("get/{key}/{db:int?}")]
        public IActionResult Get(string key, int dbIndex = 0)
        {

            var db = _redis.GetDatabase(dbIndex);
            if (key == null)
            {
                return NotFound();
            }

            var result = db.StringGet(key);

            return Ok(new { key = result.ToString() });            
        }

        [HttpPost("set/{db:int?}")]
        public IActionResult Set([FromForm]RedisString redisString, int indexDb = 0)
        {
            var db = _redis.GetDatabase(indexDb);
            Console.WriteLine($"Database changed to => {indexDb}");
            if (redisString.Key == null || redisString.Value == null)
            {
                Response.StatusCode = 400;
                return NotFound();
            }

            Console.WriteLine($"Started setting value {redisString.Key} => {redisString.Value}");
            var res = db.StringSet(redisString.Key, redisString.Value);
            Console.WriteLine("Passed\t[OK]");
            return Ok(new { key = redisString.Key, value = redisString.Value });

        }


    }
}