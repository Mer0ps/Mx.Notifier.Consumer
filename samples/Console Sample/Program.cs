using Mx.Notifier.Consumer;
using Mx.Notifier.Consumer.Models;

try
{

    var config = new RabbitConfig
    {
        HostName = "devnet-rabbitmq.beaconx.app",
        QueueName = "",
        UserName = "",
        Password = "",
        AutoAck = false,
    };

    var initializer = new RabbitMqInitializer(config);

    initializer.MessageReceivedAsync += HandleCustomMessageAsync;

    initializer.Initialize();

}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

async Task HandleCustomMessageAsync(BlockEvent? blockEvent)
{
    var filteredEvent = blockEvent?.Events ?? Enumerable.Empty<Event>();


    if (filteredEvent.Any(x => !string.IsNullOrEmpty(x.Name)))
    {
        //Do something
    }

}