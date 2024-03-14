namespace NumbersService;

public interface IGenerator
{
    int GenerateNext();
    void SetInitValue(int n);
}
