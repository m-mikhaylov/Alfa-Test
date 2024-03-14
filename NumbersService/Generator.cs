namespace NumbersService;


public class Generator: IGenerator
{

    private int _current = 1;
    private List<int> _primes = new();

    public void SetInitValue(int n)
    {
        _current = n;
        _primes = GetPrimesByEratosphene(n);
    }


    public int GenerateNext()
    {
        _current++;

        while (_primes.TakeWhile(p => p <= Math.Sqrt(_current)).Any(p => _current % p == 0))
            _current++;

        _primes.Add(_current);
        return _current;
    }


    private List<int> GetPrimesByEratosphene(int n)
    {
        var isPrime = new bool[n + 1];
        for (int i = 2; i <= n; i++) isPrime[i] = true;

        long p = 1;
        while (p <= n)
        {
            while (p <= n && !isPrime[p]) p++;
            for (long i = p * p; i <= n; i += p) isPrime[i] = false;
            p++;
        }

        return Enumerable.Range(1, n).Where(k => isPrime[k]).ToList();
    }


}
