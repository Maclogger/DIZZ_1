using DIZZ_1.BackEnd.Generators.Uniform;
using DIZZ_1.BackEnd.Simulation;

namespace DIZZ_1.BackEnd.Needle
{
    public class BuffonNeedle : AsyncSimCore<bool, int>
    {
        private readonly UniformGenerator<double> _randomY;
        private readonly UniformGenerator<double> _randomAlfa;
        private readonly double _d;
        private readonly double _l;

        public BuffonNeedle(double pD, double pL)
        {
            _d = pD;
            _l = pL;
            _randomAlfa = UniformGeneratorFactory.CreateRealUniformGenerator(0.0, 180.0);
            _randomY = UniformGeneratorFactory.CreateRealUniformGenerator(0.0, 1.0);
        }

        public async Task<double> RunCompleteSimulation(
            int replicationCount,
            IProgress<SimulationProgress<int>> progress
        )
        {
            (int cumulative, int doneReplications) = await Run(
                replicationCount,
                (total, wasInCircle) => total + (wasInCircle ? 1 : 0),
                0,
                progress
            );
            return CalculatePi(cumulative, doneReplications);
        }

        private double CalculatePi(int cumulative, int replicationCount)
        {
            double solution = (double)cumulative / replicationCount;
            return _l / (_d * solution);
        }

        protected override Task<bool> RunExperiment()
        {
            double alfaDegrees = _randomAlfa.Generate();
            double alfaRadians = alfaDegrees * (Math.PI / 180.0);

            double y = _randomY.Generate() * _d;
            double a = _l * Math.Cos(alfaRadians);

            return Task.FromResult(y + a >= _d);
        }

        public void Stop()
        {
            RequestCancellation();
        }
    }
}