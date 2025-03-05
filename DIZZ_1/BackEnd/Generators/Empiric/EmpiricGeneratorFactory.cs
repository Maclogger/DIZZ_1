using DIZZ_1.BackEnd.Generators.Discrete;
using DIZZ_1.BackEnd.Generators.Real;

namespace DIZZ_1.BackEnd.Generators.Empiric;

public static class EmpiricGeneratorFactory
{
    public static EmpiricGenerator<int> CreateDiscreteGenerator(
        List<(int min, int max, double prob)> ranges
    )
    {
        List<EmpiricDistrModel<int>> list = new();

        foreach ((int min, int max, double prob) in ranges)
        {
            list.Add(new EmpiricDistrModel<int>(
                new UniformDiscreteGenerator(min, max),
                prob
            ));
        }

        return new EmpiricGenerator<int>(list);
    }


    public static EmpiricGenerator<double> CreateRealGenerator(
        List<(double min, double max, double prob)> ranges
    )
    {
        List<EmpiricDistrModel<double>> list = new();

        foreach ((double min, double max, double prob) in ranges)
        {
            list.Add(new EmpiricDistrModel<double>(
                new UniformRealGenerator(min, max),
                prob
            ));
        }

        return new EmpiricGenerator<double>(list);
    }
}