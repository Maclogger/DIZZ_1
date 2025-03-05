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
    public UniformRealGenerator(double min, double max)
    {
        Max = max;
        Min = min;
    }

    /**
     * Generates random REAL number within given range
     * min ... INCLUSIVE
     * max ... EXCLUSIVE
     */
    protected override double GenerateValue()
    {
        //Console.WriteLine($"Min: {Min}, Max: {Max}");
        return Random.NextDouble() * (Max - Min) + Min;
    }

    public override string ToString()
    {
        return $"UniformRealGenerator: Min: {Min}, Max: {Max}";
    }
}