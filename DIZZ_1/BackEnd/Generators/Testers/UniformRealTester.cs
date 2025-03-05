using DIZZ_1.BackEnd.Generators.Real;

namespace DIZZ_1.BackEnd.Generators.Testers;

public class UniformRealTester : IGeneratorTester<double>
{
    public string DoTest(Generator<double> generator)
    {
        UniformRealGenerator gen = (UniformRealGenerator)generator;

        List<double> data = gen.Data;

        double minimum = double.MaxValue;
        double maximum = double.MinValue;
        double sum = 0;
        foreach (double value in data)
        {
            minimum = Math.Min(minimum, value);
            maximum = Math.Max(maximum, value);
            sum += value;
        }

        string sol = "Expected: \n";
        sol += $"Minimum: {gen.Min}\n";
        sol += $"Maximum: {gen.Max}\n";
        sol += $"Average: {(gen.Max - gen.Min) / 2 + gen.Min}\n";
        sol += "Actual: \n";
        sol += $"Minimum: {minimum}\n";
        sol += $"Maximum: {maximum}\n";
        sol += $"Average: {sum / data.Count}\n";
        return sol;
    }

}