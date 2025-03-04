using DIZZ_1.BackEnd.Generators.Discrete;
using DIZZ_1.BackEnd.Generators.Real;

namespace DIZZ_1.BackEnd.Generators;

public class Generators
{
    public UniformDiscreteGenerator UniformDiscreteGenerator { get; set; }
    public UniformRealGenerator UniformRealGenerator { get; set; }
    
    public Generators()
    {
        UniformDiscreteGenerator = new (0, 0, 0);
        UniformRealGenerator = new (0, 0, 0);
    }

    public int GenerateDiscreteUniform()
    {
        return UniformDiscreteGenerator.Generate();
    }
}