using DIZZ_1.BackEnd.Generators.Discrete;

namespace DIZZ_1.BackEnd.Generators.Testers;

public class UniformDiscreteTester : IGeneratorTester<int>
{
    public string DoTest(Generator<int> generator)
    {
        UniformDiscreteGenerator gen = (UniformDiscreteGenerator)generator;

        List<int> data = gen.Data;

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