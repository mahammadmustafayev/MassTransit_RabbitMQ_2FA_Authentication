using Consumer.Models;
using MassTransit;

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("rabbitmq://localhost", h =>
    {
        h.Username("guest");
        h.Password("guest");
    });

    cfg.ReceiveEndpoint("login-queue", e =>
    {
        e.Consumer<LoginEventConsumer>();
    });
});

await busControl.StartAsync();
Console.WriteLine("Consumer started. Press any key to exit");
Console.ReadKey();
await busControl.StopAsync();