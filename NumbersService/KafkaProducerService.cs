using Confluent.Kafka;

namespace NumbersService;

public class KafkaProducerService : IProducerService
{
    private readonly IPrimeFormatter _formatter;
    private readonly KafkaConfiguration _config;

    public KafkaProducerService(KafkaConfiguration config, IPrimeFormatter formatter)
    {
        _config = config;
        _formatter = formatter;
    }

    public async Task SendAsync(int number)
    {
        try
        {
            var message = _formatter.FormatMessage(number);
            var config = new ProducerConfig { BootstrapServers = _config.BootstrapServersOut };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            await producer.ProduceAsync(_config.Topic, new Message<Null, string> { Value = message });
        }
        catch { }
    }



    public int GetLastNumber()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _config.BootstrapServersIn,
            GroupId = _config.GroupId,
            AutoOffsetReset = AutoOffsetReset.Latest,
            EnableAutoCommit = true
        };

        try
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_config.Topic);
            var message = consumer.Consume(3000);

            return _formatter.GetNumberFromMessage(message.Value);
        }

        catch
        {
            return 1;
        }

    }

}
