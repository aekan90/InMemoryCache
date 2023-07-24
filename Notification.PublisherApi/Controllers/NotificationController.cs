
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IConnectionMultiplexer _redisConnection;

    public NotificationController(IConnectionMultiplexer redisConnection)
    {
        _redisConnection = redisConnection;
    }

    [HttpPost("SendNotification")]
    public async Task<IActionResult> SendNotification(string message)
    {
        var subscriber = _redisConnection.GetSubscriber();
        await subscriber.PublishAsync("myChannel", message);

        return Ok("Notification sent successfully.");
    }
}
