using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Xml.Linq;

namespace Distributed.Caching.Controllers
{
    public class ValuesController : ControllerBase
    {
        readonly IDistributedCache _distributedCache;

        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet("CacheSet")]
        public async Task<IActionResult> Set(string name, string surname)
        {
            await _distributedCache.SetStringAsync("name", name, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
            await _distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname), options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
            return Ok("Cache entry 'NAME' set successfully.");
        }

        [HttpGet("CacheGet")]
        public async Task<IActionResult> Get()
        {
            var nameValue = await _distributedCache.GetStringAsync("name");
            var sureNameBinaryValue = await _distributedCache.GetAsync("surname");
            var surname = Encoding.UTF8.GetString(sureNameBinaryValue);

            return Ok(new { nameValue, sureNameBinaryValue, surname });
        }
    }
}
