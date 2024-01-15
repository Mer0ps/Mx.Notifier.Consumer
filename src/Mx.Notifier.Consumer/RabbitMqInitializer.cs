﻿using Mx.Notifier.Consumer.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Mx.Notifier.Consumer;
public class RabbitMqInitializer
{
    private readonly RabbitConfig _config;

    public delegate void MessageReceivedHandler(BlockEvent? blockEvent);

    // Event trigger when a message is received
    public event MessageReceivedHandler MessageReceived;

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
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        //Please do not use QueueDeclare as the user lacks the authorization to generate new queues.

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            try
            {
                var body = eventArgs.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var eventData = JsonConvert.DeserializeObject<BlockEvent?>(message);

                // trigger custom logic
                OnMessageReceived(eventData);

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
        Console.ReadLine();
    }

    // Custom logic when a messag is received
    protected virtual void OnMessageReceived(BlockEvent? blockEvent)
    {
        MessageReceived?.Invoke(blockEvent);
    }
}