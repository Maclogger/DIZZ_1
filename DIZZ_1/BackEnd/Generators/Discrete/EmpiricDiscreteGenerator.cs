using System.Diagnostics;
using DIZZ_1.BackEnd.Generators.Real;

namespace DIZZ_1.BackEnd.Generators.Discrete;

public class EmpiricDiscreteGenerator: Generator<int>
{
    public List<Generator<int>> Generators { get; set; } // inclusive
    public UniformRealGenerator GeneratorChooser { get; set; }
    public List<double> Probabilities { get; set; }

    /**
     * Generates random number within given INCLUSIVE range <min, max>
     */
    public EmpiricDiscreteGenerator(
        int seed,
        List<Generator<int>> generators,
        List<double> probabilities) : base(seed)
    {
        if (probabilities.Count != generators.Count)
        {
            throw new ArgumentException("The number of probabilities must be equal to the number of generators");
        }

        Generators = generators;
        GeneratorChooser = new UniformRealGenerator(seed, 0.0, 1.0);
        Probabilities = probabilities;
    }

    public override int Generate()
    {
        Generator<int> chosenGenerator = ChooseGenerator();
        return chosenGenerator.Generate();
    }

    private Generator<int> ChooseGenerator()
    {
        double generatedNumber = GeneratorChooser.Generate();
        double sum = 0.0;

        for (int i = 0; i < Probabilities.Count; i++)
        {
            double probability = Probabilities[i];
            if (generatedNumber < sum + probability)
            {
                return Generators[i];
            }
            sum += probability;
        }

        throw new UnreachableException("The number of probabilities is out of range");
    }
}