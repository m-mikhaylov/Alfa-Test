namespace NumbersService;

public class Worker : BackgroundService
{
    private readonly IGenerator _generator;
    private readonly IProducerService _producerService;
    private readonly ILogger<Worker> _logger;

    private int _counter = 0;

    public Worker(IGenerator generator, IProducerService producerService, ILogger<Worker> logger)
    {
        _generator = generator;
        _producerService = producerService;
        _logger = logger;

        generator.SetInitValue(producerService.GetLastNumber());
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var n = _generator.GenerateNext();
            await _producerService.SendAsync(n);
            _logger.LogInformation("Worker running at: {time}, number: {number}", DateTimeOffset.Now, n);

            var delay = (++_counter) % 20 == 0 ? 1000 - DateTime.Now.Millisecond : n % 17;
            await Task.Delay(delay, stoppingToken);
        }
    }

}
