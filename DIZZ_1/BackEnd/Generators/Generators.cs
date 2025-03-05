using DIZZ_1.BackEnd.Generators.Discrete;
using DIZZ_1.BackEnd.Generators.Empiric;
using DIZZ_1.BackEnd.Generators.Real;

namespace DIZZ_1.BackEnd.Generators;

public class Generators
{
    public UniformDiscreteGenerator? UniformDiscreteGenerator { get; set; }
    public UniformRealGenerator? UniformRealGenerator { get; set; }
    public EmpiricGenerator<int>? EmpiricDiscreteGenerator { get; set; }
    public EmpiricGenerator<double>?  EmpiricRealGenerator { get; set; }
}