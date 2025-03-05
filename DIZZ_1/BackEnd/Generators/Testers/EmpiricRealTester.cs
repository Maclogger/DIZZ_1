using DIZZ_1.BackEnd.Generators.Empiric;
using DIZZ_1.BackEnd.Generators.Real;

namespace DIZZ_1.BackEnd.Generators.Testers;

public class EmpiricRealTester : IGeneratorTester<double>
{
    public EmpiricRealTester()
    {
    }

    public string DoTest(Generator<double> generator)
    {
        EmpiricGenerator<double> gen = (EmpiricGenerator<double>)generator;

        string expectedOutput = GenerateExpectedOutput(gen);
        string actualOutput = GenerateActualOutput(gen);

        return expectedOutput + actualOutput;
    }

    private static string GenerateExpectedOutput(EmpiricGenerator<double> gen)
    {
        string result = "Expected: \n";

        foreach (EmpiricDistrModel<double> distrModel in gen.DistrModels)
        {
            result += FormatExpectedGroup(distrModel);
        }

        return result;
    }

    private static string FormatExpectedGroup(EmpiricDistrModel<double> distrModel)
    {
        UniformRealGenerator uniformRealGen = (UniformRealGenerator)distrModel.Generator;
        return $"<{uniformRealGen.Min}; {uniformRealGen.Max}) - {distrModel.Probability}\n";
    }

    private static string GenerateActualOutput(EmpiricGenerator<double> gen)
    {
        string result = "Actual: \n";

        Dictionary<(double Min, double Max), double> groupPercentages =
            CalculateGroupPercentages(gen);

        foreach (KeyValuePair<(double Min, double Max), double> group in groupPercentages)
        {
            result += FormatActualGroup(group);
        }

        return result;
    }

    private static string FormatActualGroup(KeyValuePair<(double Min, double Max), double> group)
    {
        return $"<{group.Key.Min}; {group.Key.Max}) - {group.Value:F4}\n";
    }

    private static Dictionary<(double Min, double Max), double> CalculateGroupPercentages(
        EmpiricGenerator<double> gen)
    {
        Dictionary<(double Min, double Max), int> groupCounts = InitializeGroupCounts(gen);
        CountDataInGroups(gen, groupCounts);
        return ConvertCountsToPercentages(gen, groupCounts);
    }

    private static Dictionary<(double Min, double Max), int> InitializeGroupCounts(
        EmpiricGenerator<double> gen)
    {
        var groupCounts = new Dictionary<(double Min, double Max), int>();

        foreach (EmpiricDistrModel<double> distrModel in gen.DistrModels)
        {
            UniformRealGenerator uniformRealGen = (UniformRealGenerator)distrModel.Generator;
            (double Min, double Max) range = (uniformRealGen.Min, uniformRealGen.Max);
            groupCounts[range] = 0;
        }

        return groupCounts;
    }

    private static void CountDataInGroups(
        EmpiricGenerator<double> gen,
        Dictionary<(double Min, double Max), int> groupCounts)
    {
        foreach (double value in gen.Data)
        {
            foreach ((double Min, double Max) range in groupCounts.Keys)
            {
                if (IsValueInRange(value, range))
                {
                    groupCounts[range]++;
                    break;
                }
            }
        }
    }

    private static bool IsValueInRange(double value, (double Min, double Max) range)
    {
        return value >= range.Min && value < range.Max;
    }

    private static Dictionary<(double Min, double Max), double> ConvertCountsToPercentages(
        EmpiricGenerator<double> gen,
        Dictionary<(double Min, double Max), int> groupCounts)
    {
        int totalDataPoints = gen.Data.Count;

        Dictionary<(double Min, double Max), double> groupPercentages =
            new Dictionary<(double Min, double Max), double>();

        foreach (KeyValuePair<(double Min, double Max), int> group in groupCounts)
        {
            groupPercentages[group.Key] = CalculatePercentage(group.Value, totalDataPoints);
        }

        return groupPercentages;
    }

    private static double CalculatePercentage(int count, int total)
    {
        return (double)count / total;
    }
}