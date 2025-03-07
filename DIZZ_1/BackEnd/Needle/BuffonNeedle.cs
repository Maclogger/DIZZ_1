using System;
using System.Threading.Tasks;
using DIZZ_1.BackEnd.Generators.Real;
using DIZZ_1.BackEnd.Simulation;
using DIZZ_1.Components.Chart;

namespace DIZZ_1.BackEnd.Needle
{
    public class BuffonNeedle : SimCoreWithChart
    {
        public Func<double, Task> UpdatePi { get; }
        private readonly UniformRealGenerator _randomY;
        private readonly UniformRealGenerator _randomAlfa;
        private readonly double _d;
        private readonly double _l;

        public BuffonNeedle(double pD, double pL, Func<double, Task> updatePi,
            RealTimeChart chart)
        {
            UpdatePi = updatePi;
            _d = pD;
            _l = pL;
            _randomAlfa = new UniformRealGenerator(0.0, 180.0);
            _randomY = new UniformRealGenerator(0.0, 1.0);
            RealTimeChart = chart;
        }

        public override void BeforeSimulation()
        {
            // Môžete tu pridať inicializačný kód pred simuláciou.
        }

        public override void BeforeSimulationRun(int currentRun)
        {
            // Môžete tu pridať kód spustený pred každým experimentom.
        }

        public override void AfterSimulation(double solution)
        {
            Console.WriteLine("AfterSimulation");
        }

        public override void AfterSimulationRun(double solution, int currentRun)
        {
            Console.WriteLine("AfterSimulationRun");
            double newPi = CalculatePi(solution);
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

        public async Task<double> DoExperiment(int replicationCount)
        {
            double solution = await Run(replicationCount);
            return CalculatePi(solution);
        }

        private double CalculatePi(double solution)
        {
            // Predchádzajúca formula predpokladá, že solution nie je nulová.
            return _l / (_d * solution);
        }

        public override RealTimeChart RealTimeChart { get; }
        public override double ToChartValue(double solution)
        {
            return CalculatePi(solution);
        }
    }
}
