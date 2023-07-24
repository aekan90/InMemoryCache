// S U B S C R I B E R
using StackExchange.Redis;

ConnectionMultiplexer redisConnection = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber subscriber = redisConnection.GetSubscriber();

// MyChannel.* --> MyChannel. ile başlayan tüm gruplara abone ol.
// MyChannel.Grup1, MyChannel.Grup2 gibi tüm gruplara üye.
await subscriber.SubscribeAsync("MyChannel.*", (channel, message) =>
{
    Console.WriteLine("myChannel : " + message);
});

Console.WriteLine("SUBSCRIBER");
Console.ReadLine();