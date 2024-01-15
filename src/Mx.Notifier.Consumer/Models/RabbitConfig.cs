namespace Mx.Notifier.Consumer.Models;
public class RabbitConfig
{
    public string HostName { get; set; }
    public string QueueName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool AutoAck { get; set; }
}
