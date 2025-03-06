using DIZZ_1.BackEnd.Generators.Real;
using DIZZ_1.BackEnd.Simulation;

namespace DIZZ_1.BackEnd.Needle;

public class BuffonNeedle : SimCore
{
    public Func<double, Task> UpdatePi { get; }
    private readonly UniformRealGenerator _randomY;
    private readonly UniformRealGenerator _randomAlfa;
    private readonly double _d;
    private readonly double _l;

    public BuffonNeedle(double pD, double pL, Func<double, Task> updatePi)
    {
        UpdatePi = updatePi;
        _d = pD;
        _l = pL;
        _randomAlfa = new UniformRealGenerator(0.0, 180.0);
        _randomY = new UniformRealGenerator(0.0, 1.0);
    }


    public override void BeforeSimulation()
    {
    }

    public override void BeforeSimulationRun()
    {
    }

    public override void AfterSimulation(double solution)
    {
        Console.WriteLine("AfterSimulation");
    }

    public override void AfterSimulationRun(double solution)
    {
        Console.WriteLine("AfterSimulationRun");
        double newPi =CalculatePi(solution);
        UpdatePi.Invoke(newPi);
    }

    public override double RunExperiment()
    {
        double alfaDegrees = _randomAlfa.Generate();
        double alfaRadians = alfaDegrees * (Math.PI / 180.0);

        double y = _randomY.Generate() * _d;

        double a = _l * Math.Cos(alfaRadians);

        return y + a >= _d ? 1.0 : 0.0;
    }

    public double DoExperiment(int replicationCount)
    {
        double solution = Run(replicationCount);
        return CalculatePi(solution);
    }

    private double CalculatePi(double solution)
    {
        double piApproximate = _l / (_d * solution);
        return piApproximate;
    }
}