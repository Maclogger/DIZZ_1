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
    public UniformDiscreteGenerator(int min, int max)
    {
        Min = min;
        Max = max;
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
    protected override int GenerateValue()
    {
        /*
        Console.WriteLine($"UniformDiscreteGenerator: {Min}-{Max}");
        */
        return Random.Next(Min, Max);
    }

    public override string ToString()
    {
        return $"UniformDiscreteGenerator: Min: {Min}, Max: {Max}";
    }
}