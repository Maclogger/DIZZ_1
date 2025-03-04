namespace DIZZ_1.BackEnd.Generators.Discrete;

public class UniformDiscreteGenerator : Generator<int>
{
    public int Min { get; set; } // inclusive
    public int Max { get; set; } // exclusive

    /**
     * Generates random INT number within given range
     * min ... INCLUSIVE
     * max ... EXCLUSIVE
     */
    public UniformDiscreteGenerator(int seed, int min, int max) : base(seed)
    {
        Min = min;
        Max = max;
        Console.WriteLine($"UniformDiscreteGenerator: {seed}-{min}-{max}");
    }

    /**
     * Generates random INT number within given range
     * min ... INCLUSIVE
     * max ... EXCLUSIVE
     */
    public int GenerateInRange(int min, int max)
    {
        return Random.Next(min, max);
    }

    /**
     * Generates random INT number within given range
     * min ... INCLUSIVE
     * max ... EXCLUSIVE
     */
    public override int Generate()
    {
        Console.WriteLine($"Generujem: {Min}-{Max}");
        return Random.Next(Min, Max);
    }
}