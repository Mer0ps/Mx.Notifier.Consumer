using Mx.NET.SDK.Core.Domain.Helper;

namespace Mx.Notifier.Consumer.Models;

public class BlockEvent
{
    public IEnumerable<Event> Events { get; set; }
    public string Hash { get; set; }
    public string ShardId { get; set; }
    public int Timestamp { get; set; }
}
public class Event
{
    public string Address { get; set; }
    public string? Data { get; set; }
    public string? Identifier { get; set; }
    public IEnumerable<string> Topics { get; set; }
    public string TxHash { get; set; }

    public string? Name
    {
        get
        {
            if (Identifier == null || IsTransactionEvent(Identifier) || Topics == null || !Topics.Any())
            {
                return null;
            }

            return DataCoder.DecodeData(Topics.First());
        }
    }

    private static bool IsTransactionEvent(string identifier)
    {
        return typeof(TransactionEvents).GetFields()
            .Any(field => (string)field.GetValue(null) == identifier);
    }
}


