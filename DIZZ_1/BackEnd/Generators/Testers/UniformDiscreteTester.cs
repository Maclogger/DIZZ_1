using DIZZ_1.BackEnd.Generators.Uniform;

namespace DIZZ_1.BackEnd.Generators.Testers;

public class UniformDiscreteTester : IGeneratorTester<int>
{
    public string DoTest(Generator<int> generator)
    {
        UniformGenerator<int> gen = (UniformGenerator<int>)generator;

        List<int>? data = gen.Data;
        if (data is null)
        {
            throw new ArgumentException("Generator did not have History Enabled.");
        }

        int minimum = int.MaxValue;
        int maximum = int.MinValue;
        int sum = 0;
        foreach (int value in data)
        {
            minimum = Math.Min(minimum, value);
            maximum = Math.Max(maximum, value);
            sum += value;
        }

        string sol = "Expected: \n";
        sol += $"Minimum: {gen.Min}\n";
        sol += $"Maximum: {gen.Max}\n";
        sol += $"Average: {(double)(gen.Max - gen.Min) / 2 + gen.Min}\n";
        sol += "Actual: \n";
        sol += $"Minimum: {minimum}\n";
        sol += $"Maximum: {maximum}\n";
        sol += $"Average: {(double)sum / data.Count}\n";
        return sol;
    }
}