namespace NumbersService;

public interface IProducerService
{
    Task SendAsync(int number);
    int GetLastNumber();
}
