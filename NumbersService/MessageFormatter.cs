using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NumbersService;

public record KafkaMessage(DateTime timestamp_generated, int number);


public class MessageFormatter: IPrimeFormatter
{
    public string FormatMessage(int number)
    {
        var message = new KafkaMessage(DateTime.Now, number);
        return JsonSerializer.Serialize(message);
    }

    public int GetNumberFromMessage(string message)
    {
        var record = JsonSerializer.Deserialize<KafkaMessage>(message);
        return record?.number ?? 0;
    }

}
