using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("CacheGet")]
        public IActionResult Get()
        {
            string cachedValue = _memoryCache.Get<string>("NAME");

            if (cachedValue == null)
            {
                return NotFound("The cache entry with key 'NAME' does not exist.");
            }

            return Ok(cachedValue);
        }

        [HttpPost("CacheSet")]
        public IActionResult Set([FromBody] string value)
        {
            _memoryCache.Set("NAME", value);
            return Ok("Cache entry 'NAME' set successfully.");
        }

        [HttpGet("CacheDelete")]
        public IActionResult Delete()
        {
            _memoryCache.Remove("NAME");
            return Ok("Cache entry deleted successfully.");
        }

    }
}
