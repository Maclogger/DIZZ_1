using DIZZ_1.BackEnd.Generators.Testers;

namespace DIZZ_1.BackEnd.Generators.Uniform;

public class UniformGenerator<T> : Generator<T>
{
    public T Min { get; set; }
    public T Max { get; set; }

    private readonly Func<Random, T, T, T> _generatorFunc;

    public UniformGenerator(T min, T max, Func<Random, T, T, T> generatorFunc)
    {
        Min = min;
        Max = max;
        _generatorFunc = generatorFunc;
    }

    protected override T GenerateValue()
    {
        return _generatorFunc(Random, Min, Max);
    }

    public override IGeneratorTester<T>? GetTester()
    {
        if (this is UniformGenerator<int>)
        {
            return new UniformDiscreteTester() as IGeneratorTester<T>;
        }
        return new UniformRealTester() as IGeneratorTester<T>;
    }

}