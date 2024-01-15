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

    initializer.MessageReceived += HandleCustomMessage;

    initializer.Initialize();

}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

static void HandleCustomMessage(BlockEvent? blockEvent)
{
    var filteredEvent = blockEvent?.Events ?? Enumerable.Empty<Event>();


    if (filteredEvent.Any(x => !string.IsNullOrEmpty(x.Name)))
    {
        //Do something
    }

}