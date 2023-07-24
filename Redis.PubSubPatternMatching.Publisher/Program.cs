// P U B L İ S H E R
using StackExchange.Redis;

ConnectionMultiplexer redisConnection = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber subscriber = redisConnection.GetSubscriber();

while (true)
{
    Console.WriteLine("PUBLISHER");
    Console.Write("Mesaj : ");
    string message = Console.ReadLine();
    await subscriber.PublishAsync("MyChannel.*", message);
}