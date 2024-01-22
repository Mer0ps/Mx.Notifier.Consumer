using Mx.Notifier.Consumer.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Mx.Notifier.Consumer;
public class RabbitMqInitializer
{
    private readonly RabbitConfig _config;

    public delegate Task MessageReceivedHandler(BlockEvent? blockEvent);

    // Event trigger when a message is received
    public event MessageReceivedHandler MessageReceivedAsync;

    public RabbitMqInitializer(RabbitConfig config)
    {
        _config = config;
    }

    public void Initialize()
    {
        var factory = new ConnectionFactory
        {
            HostName = _config.HostName,
            UserName = _config.UserName,
            Password = _config.Password,
            DispatchConsumersAsync = true
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        //Please do not use QueueDeclare as the user lacks the authorization to generate new queues.

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += async (model, eventArgs) =>
        {
            try
            {
                var body = eventArgs.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var eventData = JsonConvert.DeserializeObject<BlockEvent?>(message);

                // trigger custom logic
                await OnMessageReceivedAsync(eventData);

                //Ack the message when done.
                channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Requeue the message
                channel.BasicNack(eventArgs.DeliveryTag, false, true);
            }

        };

        channel.BasicConsume(queue: _config.QueueName, autoAck: _config.AutoAck, consumer: consumer);

        Console.WriteLine($"Consumer with queue name {_config.QueueName} is initialized.");
    }

    // Custom logic when a message is received
    protected virtual async Task OnMessageReceivedAsync(BlockEvent? blockEvent)
    {
        if (MessageReceivedAsync != null)
        {
            await MessageReceivedAsync?.Invoke(blockEvent);
        }
    }
}
