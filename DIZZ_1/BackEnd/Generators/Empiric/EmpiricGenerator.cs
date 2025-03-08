using System.Diagnostics;
using DIZZ_1.BackEnd.Generators.Testers;
using DIZZ_1.BackEnd.Generators.Uniform;

namespace DIZZ_1.BackEnd.Generators.Empiric;

public class EmpiricDistrModel<T>
{
    public Generator<T> Generator;
    public double Probability;

    public EmpiricDistrModel(Generator<T> generator, double probability)
    {
        Generator = generator;
        Probability = probability;
    }
}

public class EmpiricGenerator<T> : Generator<T>
{
    public List<EmpiricDistrModel<T>> DistrModels;
    public UniformGenerator<double> DistrChooserGenerator { get; set; }


    public EmpiricGenerator(List<EmpiricDistrModel<T>> distrModels)
    {
        DistrModels = distrModels;
        DistrChooserGenerator = UniformGeneratorFactory.CreateRealUniformGenerator(0.0, 1.0);
    }

    protected override T GenerateValue()
    {
        Generator<T> chosenGenerator = ChooseGenerator();
        return chosenGenerator.Generate();
    }

    public override IGeneratorTester<T>? GetTester()
    {
        if (this is EmpiricGenerator<int>)
        {
            return new EmpiricDiscreteTester() as IGeneratorTester<T>;
        }
        return new EmpiricRealTester() as IGeneratorTester<T>;
    }

    private Generator<T> ChooseGenerator()
    {
        double generatedNumber = DistrChooserGenerator.Generate();
        double sum = 0.0;

        for (int i = 0; i < DistrModels.Count; i++)
        {
            double probability = DistrModels[i].Probability;
            if (generatedNumber < sum + probability)
            {
                return DistrModels[i].Generator;
            }

            sum += probability;
        }

        throw new UnreachableException("The number of probabilities is out of range");
    }

    public override string ToString()
    {
        string sol = $"EmpiricGenerator<{typeof(T).Name}>: Probs:\n";
        foreach (EmpiricDistrModel<T> distrModel in DistrModels)
        {
            sol += $"{distrModel.Generator} - {distrModel.Probability}\n";
        }

        return sol;
    }
}