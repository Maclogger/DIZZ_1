namespace DIZZ_1.BackEnd.Generators;

public abstract class Generator<T>
{
    public Random Random { get; set; }
    int Seed { get; set; } = 0;
    public List<T> Data { get; set; } = new();

    protected Generator()
    {
        Seed = MainApp.Instance.MasterSeeder.Next();
        Random = new Random(Seed);
    }

    protected abstract T GenerateValue();

    public T Generate()
    {
        T value = GenerateValue();
        Data.Add(value);
        return value;
    }
}