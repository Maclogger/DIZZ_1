using DIZZ_1.BackEnd.Generators.Testers;

namespace DIZZ_1.BackEnd.Generators;

public abstract class Generator<T>
{
    public Random Random { get; set; }
    int Seed { get; set; } = 0;

    protected Generator()
    {
        Seed = MainApp.Instance.MasterSeeder.Next();
        Random = new Random(Seed);
    }

    protected abstract T GenerateValue();

    public T Generate()
    {
        T value = GenerateValue();
        if (Data is not null)
        {
            Data.Add(value);
        }
        return value;
    }
    public abstract IGeneratorTester<T>? GetTester();

    public List<T>? Data { get; set; } = null;
    public void EnableHistory()
    {
        Data = new List<T>();
    }
}