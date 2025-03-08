using DIZZ_1.BackEnd.Generators.Discrete;
using DIZZ_1.BackEnd.Generators.Empiric;

namespace DIZZ_1.BackEnd.Generators.Testers;

public class EmpiricDiscreteTester : IGeneratorTester<int>
{
    public EmpiricDiscreteTester()
    {
    }

    public string DoTest(Generator<int> generator)
    {
        EmpiricGenerator<int> gen = (EmpiricGenerator<int>)generator;

        string expectedOutput = GenerateExpectedOutput(gen);
        string actualOutput = GenerateActualOutput(gen);

        return expectedOutput + actualOutput;
    }

    private static string GenerateExpectedOutput(EmpiricGenerator<int> gen)
    {
        string result = "Expected: \n";

        foreach (EmpiricDistrModel<int> distrModel in gen.DistrModels)
        {
            result += FormatExpectedGroup(distrModel);
        }

        return result;
    }

    private static string FormatExpectedGroup(EmpiricDistrModel<int> distrModel)
    {
        UniformDiscreteGenerator uniformDiscreteGen = (UniformDiscreteGenerator)distrModel.Generator;
        return $"<{uniformDiscreteGen.Min}; {uniformDiscreteGen.Max}) - {distrModel.Probability}\n";
    }

    private static string GenerateActualOutput(EmpiricGenerator<int> gen)
    {
        string result = "Actual: \n";

        Dictionary<(int Min, int Max), double> groupPercentages =
            CalculateGroupPercentages(gen);

        foreach (KeyValuePair<(int Min, int Max), double> group in groupPercentages)
        {
            result += FormatActualGroup(group);
        }

        return result;
    }

    private static string FormatActualGroup(KeyValuePair<(int Min, int Max), double> group)
    {
        return $"<{group.Key.Min}; {group.Key.Max}) - {group.Value:F4}\n";
    }

    private static Dictionary<(int Min, int Max), double> CalculateGroupPercentages(
        EmpiricGenerator<int> gen)
    {
        Dictionary<(int Min, int Max), int> groupCounts = InitializeGroupCounts(gen);
        CountDataInGroups(gen, groupCounts);
        return ConvertCountsToPercentages(gen, groupCounts);
    }

    private static Dictionary<(int Min, int Max), int> InitializeGroupCounts(
        EmpiricGenerator<int> gen)
    {
        var groupCounts = new Dictionary<(int Min, int Max), int>();

        foreach (EmpiricDistrModel<int> distrModel in gen.DistrModels)
        {
            UniformDiscreteGenerator uniformDiscreteGen = (UniformDiscreteGenerator)distrModel.Generator;
            (int Min, int Max) range = (uniformDiscreteGen.Min, uniformDiscreteGen.Max);
            groupCounts[range] = 0;
        }

        return groupCounts;
    }

    private static void CountDataInGroups(
        EmpiricGenerator<int> gen,
        Dictionary<(int Min, int Max), int> groupCounts)
    {
        if (gen.Data is null)
        {
            throw new ArgumentException("Generator did not have History Enabled.");
        }

        foreach (int value in gen.Data)
        {
            foreach ((int Min, int Max) range in groupCounts.Keys)
            {
                bool isValueInRange = value >= range.Min && value < range.Max;
                if (isValueInRange)
                {
                    groupCounts[range]++;
                    break;
                }
            }
        }
    }

    private static Dictionary<(int Min, int Max), double> ConvertCountsToPercentages(
        EmpiricGenerator<int> gen,
        Dictionary<(int Min, int Max), int> groupCounts)
    {
        if (gen.Data is null)
        {
            throw new ArgumentException("Generator did not have History Enabled.");
        }

        int totalDataPoints = gen.Data.Count;

        Dictionary<(int Min, int Max), double> groupPercentages = new();

        foreach (KeyValuePair<(int Min, int Max), int> group in groupCounts)
        {
            groupPercentages[group.Key] = (double)group.Value / totalDataPoints;
        }

        return groupPercentages;
    }
}