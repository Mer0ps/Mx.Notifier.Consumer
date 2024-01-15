## About

Mx.Notifier.Consumer is a C# library to help you setup a consumer for the MultiversX blockchain.

More documentation about Notifier service is available here [Events notifier](https://docs.multiversx.com/sdk-and-tools/notifier/).

## How to Use

```csharp
// set these values correctly for your database server
var config = new RabbitConfig
{
    HostName = "server-hostname",
    QueueName = "queue-name",
    UserName = "username",
    Password = "Password",
    AutoAck = false,
};

// initialize your consumer
var initializer = new RabbitMqInitializer(config);

// Setup your custom code (ie: listen transaction of a specific smartcontract etc..)
initializer.MessageReceived += HandleCustomMessage;

// Consume
initializer.Initialize();


// Custom method
static void HandleCustomMessage(BlockEvent? blockEvent)
{
    var filteredEvent = blockEvent?.Events ?? Enumerable.Empty<Event>();


    if (filteredEvent.Any(x => !string.IsNullOrEmpty(x.Name)))
    {
        //Do something
    }

}

```


## Feedback

Mx.Notifier.Consumer is released as open source under the [MIT license](https://github.com/Mer0ps/Mx.Notifier.Consumer/blob/master/src/Mx.Notifier.Consumer/LICENSE.txt). Bug reports and contributions are welcome at [the GitHub repository](https://github.com/Mer0ps/Mx.Notifier.Consumer).
