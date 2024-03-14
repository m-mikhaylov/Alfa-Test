namespace NumbersService;

public class Program
{
    static void Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddTransient<IGenerator, Generator>();
                services.AddTransient<IProducerService, KafkaProducerService>();
                services.AddTransient<IPrimeFormatter, MessageFormatter>();
                var kafkaConfiguration = context.Configuration.GetSection("KafkaConfiguration").Get<KafkaConfiguration>();
                services.AddSingleton(kafkaConfiguration);

                services.AddHostedService<Worker>();
            })
            .Build();

        host.Run();
    }

}