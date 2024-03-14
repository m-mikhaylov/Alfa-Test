namespace NumbersService;

public interface IPrimeFormatter
{
    string FormatMessage(int number);
    int GetNumberFromMessage(string message);
}
