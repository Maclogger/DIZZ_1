using DIZZ_1.BackEnd.Generators.Real;
using DIZZ_1.BackEnd.Simulation;
using DIZZ_1.Components.Charts;

namespace DIZZ_1.BackEnd.Needle;

public class BuffonNeedle : SimCore
{
    public event Action<double, int>? OnApproximationUpdated;
    private readonly UniformRealGenerator _randomY;
    private readonly UniformRealGenerator _randomAlfa;
    private readonly double _d;
    private readonly double _l;

    public BuffonNeedle(double pD, double pL, Action<double, int>? onApproximationUpdated = null)
    {
        OnApproximationUpdated = onApproximationUpdated;
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

    public override void AfterSimulation(double cumulative)
    {

    }

    public override void AfterSimulationRun(double cumulative)
    {
        if (cumulative > 0)
        {
            Console.WriteLine($"Cumulative: {cumulative}");
            Console.WriteLine($"_l: {_l}, _d: {_d}");
            int currentRun = CurrentRun;
            Console.WriteLine($"Current run: {currentRun}");
            double currentPi = _l / (_d * (cumulative / currentRun));
            Console.WriteLine($"Buffon: {currentPi} - {currentRun}");
            OnApproximationUpdated?.Invoke(currentPi, currentRun);
        }
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

        double piAproxima = _l / (_d * solution);

        return piAproxima;
        /*
            Console.WriteLine($"Vysledok experimentu: {solution}");
            Console.WriteLine($"Pi aproxima: {piAproxima}");
        */
    }
}