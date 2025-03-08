using DIZZ_1.BackEnd.Generators.Uniform;
using DIZZ_1.BackEnd.Simulation;
using DIZZ_1.Components.Chart;

namespace DIZZ_1.BackEnd.Needle
{
    public class BuffonNeedle : SimCore
    {
        public Func<double, Task> UpdatePi { get; }
        private readonly UniformGenerator<double> _randomY;
        private readonly UniformGenerator<double> _randomAlfa;
        private readonly double _d;
        private readonly double _l;

        public BuffonNeedle(double pD, double pL, Func<double, Task> updatePi,
            RealTimeChart chart)
        {
            UpdatePi = updatePi;
            _d = pD;
            _l = pL;
            _randomAlfa = UniformGeneratorFactory.CreateRealUniformGenerator(0.0, 180.0);
            _randomY = UniformGeneratorFactory.CreateRealUniformGenerator(0.0, 1.0);
            RealTimeChart = chart;
        }

        protected override void BeforeSimulation()
        {
            // Môžete tu pridať inicializačný kód pred simuláciou.
        }

        protected override void BeforeReplication()
        {
            // Môžete tu pridať kód spustený pred každým experimentom.
        }

        protected override void AfterSimulation(double solution)
        {
            Console.WriteLine("AfterSimulation");
        }

        protected override void AfterReplication(double solution)
        {
            Console.WriteLine("AfterSimulationRun");
            double newPi = CalculatePi(solution);
            UpdatePi.Invoke(newPi);
        }

        protected override double RunExperiment()
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
            // Predchádzajúca formula predpokladá, že solution nie je nulová.
            return _l / (_d * solution);
        }

        public RealTimeChart RealTimeChart { get; }
        public double ToChartValue(double solution)
        {
            return CalculatePi(solution);
        }
    }
}
