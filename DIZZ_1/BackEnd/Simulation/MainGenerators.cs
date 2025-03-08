using DIZZ_1.BackEnd.Generators.Empiric;
using DIZZ_1.BackEnd.Generators.Uniform;

namespace DIZZ_1.BackEnd.Simulation;

public class MainGenerators
{
    // INPUT:
    public EmpiricGenerator<double> Supp2From16Gen { get; set; }

    public EmpiricGenerator<double> Supp2First15Gen { get; set; }

    public UniformGenerator<double> Supp1From11Gen { get; set; }

    public UniformGenerator<double> Supp1First10Gen { get; set; }

    public UniformGenerator<double> DeliveryGen { get; set; }

    // OUTPUT:
    public EmpiricGenerator<int> LightsGen { get; set; }

    public UniformGenerator<int> BrakePadsGen { get; set; }

    public UniformGenerator<int> AbsorbersGen { get; set; }


    public MainGenerators()
    {
        InitializeInputGenerators();
        InitializeOutputGenerators();
    }

    private void InitializeInputGenerators()
    {
        Supp1First10Gen = UniformGeneratorFactory.CreateRealUniformGenerator(10.0, 70.0);
        Supp1From11Gen = UniformGeneratorFactory.CreateRealUniformGenerator(30.0, 95.0);

        Supp2First15Gen = EmpiricGeneratorFactory.CreateRealGenerator([
            (5, 10, 0.4),
            (10, 50, 0.3),
            (50, 70, 0.2),
            (70, 80, 0.06),
            (80, 95, 0.04),
        ]);
        Supp2From16Gen = EmpiricGeneratorFactory.CreateRealGenerator([
            (5, 10, 0.2),
            (10, 50, 0.4),
            (50, 70, 0.3),
            (70, 80, 0.06),
            (80, 95, 0.04),
        ]);

        DeliveryGen = UniformGeneratorFactory.CreateRealUniformGenerator(0.0, 100.0);
    }


    private void InitializeOutputGenerators()
    {
        AbsorbersGen = UniformGeneratorFactory.CreateDiscreteUniformGenerator(50, 101);
        BrakePadsGen = UniformGeneratorFactory.CreateDiscreteUniformGenerator(60, 251);
        LightsGen = EmpiricGeneratorFactory.CreateDiscreteGenerator([
            (30, 60, 0.2),
            (60, 100, 0.4),
            (100, 140, 0.3),
            (140, 160, 0.1),
        ]);
    }
}