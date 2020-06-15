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
    [Route("api/")]
    [ApiController]
    [Produces("application/json")]
    public class RedisController : Controller
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisController(ConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        [HttpGet("get/{key}/{db:int?}")]
        public JsonResult Get(string key, int dbIndex = 0)
        {

            var db = _redis.GetDatabase(dbIndex);
            if (key == null)
            {
                Response.StatusCode = 400;
                return Json(new
                {
                    status = "Error not found key"
                });
            }

            RedisValue result = db.StringGet(key);

            
            return Json(new { value = result.ToString() });
        }
        [HttpPost("set/{db:int?}")]
        
        public JsonResult Set([FromForm]RedisString redisString, int indexDb = 0)

        {
            var db = _redis.GetDatabase(indexDb);
            Console.WriteLine($"Database changed to => {indexDb}");
            if (redisString.Key == null || redisString.Value == null)
            {
                Response.StatusCode = 400;
                return Json(new { status = "Not fount value" });
            }

            Console.WriteLine($"Started setting value {redisString.Key} => {redisString.Value}");
            var res = db.StringSet(redisString.Key, redisString.Value);
            Console.WriteLine("Passed\t[OK]");
            return Json(new { key = redisString.Key, value = redisString.Value });

        }


    }
}