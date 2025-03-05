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
    public abstract T Generate();
}