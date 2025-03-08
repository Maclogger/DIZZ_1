using DIZZ_1.BackEnd.Generators.Real;

namespace DIZZ_1.BackEnd.Generators.Testers;

public class UniformRealTester : IGeneratorTester<double>
{
    public string DoTest(Generator<double> generator)
    {
        UniformRealGenerator gen = (UniformRealGenerator)generator;

        List<double>? data = gen.Data;
        if (data is null)
        {
            throw new ArgumentException("Generator did not have History Enabled.");
        }

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
        sol += $"Minimum: {gen.Min.ToString(Config.FloatFormat)}\n";
        sol += $"Maximum: {gen.Max.ToString(Config.FloatFormat)}\n";
        sol += $"Average: {((gen.Max - gen.Min) / 2 + gen.Min).ToString(Config.FloatFormat)}\n";
        sol += "Actual: \n";
        sol += $"Minimum: {minimum.ToString(Config.FloatFormat)}\n";
        sol += $"Maximum: {maximum.ToString(Config.FloatFormat)}\n";
        sol += $"Average: {(sum / data.Count).ToString(Config.FloatFormat)}\n";
        return sol;
    }
}