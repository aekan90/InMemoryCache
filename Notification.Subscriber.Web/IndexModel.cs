using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace NotificationWeb//.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IHubContext<NotificationHub> _hubContext;

        public IndexModel(IConnectionMultiplexer redisConnection, IHubContext<NotificationHub> hubContext)
        {
            _redisConnection = redisConnection;
            _hubContext = hubContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSendNotificationAsync(string message)
        {
            var subscriber = _redisConnection.GetSubscriber();
            await subscriber.PublishAsync("myChannel", message);

            return RedirectToPage();
        }
    }
}
