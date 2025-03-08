namespace DIZZ_1.BackEnd.Generators.Uniform;

public static class UniformGeneratorFactory
{
    public static UniformGenerator<double> CreateRealUniformGenerator(double min, double max)
    {
        return new UniformGenerator<double>(min, max, (random, pMin, pMax) =>
        {
            return random.NextDouble() * (pMax - pMin) + pMin;
        });
    }

    public static UniformGenerator<int> CreateDiscreteUniformGenerator(int min, int max)
    {
        return new UniformGenerator<int>(min, max, (random, pMin, pMax) =>
        {
            return random.Next(pMin, pMax);
        });
    }
}