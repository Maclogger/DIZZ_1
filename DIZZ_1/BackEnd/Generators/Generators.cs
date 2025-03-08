using DIZZ_1.BackEnd.Generators.Empiric;
using DIZZ_1.BackEnd.Generators.Uniform;

namespace DIZZ_1.BackEnd.Generators;

public class Generators
{
    // BASIC Testers
    public UniformGenerator<int>? UniformDiscreteGenerator { get; set; }
    public UniformGenerator<double>? UniformRealGenerator { get; set; }
    public EmpiricGenerator<int>? EmpiricDiscreteGenerator { get; set; }
    public EmpiricGenerator<double>?  EmpiricRealGenerator { get; set; }

    // Semester work
    public void InitializeAll()
    {

    }
}