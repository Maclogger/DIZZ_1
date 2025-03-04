namespace DIZZ_1.BackEnd.Generators.Real;

public class UniformRealGenerator : Generator<double>
{
    public double Min { get; set; }
    public double Max { get; set; }

    /**
     * Generates random REAL number within given range
     * min ... INCLUSIVE
     * max ... EXCLUSIVE
     */
    public UniformRealGenerator(int seed, double min, double max) : base(seed)
    {
        Max = max;
        Min = min;
        Console.WriteLine($"UniformRealGenerator: {seed}-{min}-{max}");
    }

    /**
     * Generates random REAL number within given range
     * min ... INCLUSIVE
     * max ... EXCLUSIVE
     */
    public override double Generate()
    {
        Console.WriteLine($"Min: {Min}, Max: {Max}");
        return Random.NextDouble() * (Max - Min) + Min;
    }
}