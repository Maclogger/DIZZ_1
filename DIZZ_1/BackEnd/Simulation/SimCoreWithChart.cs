/*
using System.Threading.Tasks;
using DIZZ_1.Components.Chart;

namespace DIZZ_1.BackEnd.Simulation
{
    public abstract class SimCoreWithChart : SimCore
    {
        public abstract RealTimeChart RealTimeChart { get; }
        public abstract double ToChartValue(double solution);

        public new async Task<double> Run(int replicationCount)
        {
            int nthPointToDraw = Config.GetNthPointToDraw(replicationCount);

            BeforeSimulation();
            double cumulative = 0;
            for (int i = 0; i < replicationCount; i++)
            {
                BeforeReplication(i + 1);
                double experimentResult = RunExperiment();
                cumulative += experimentResult;
                AfterReplication(cumulative / (i + 1), i + 1);

                if (i % nthPointToDraw == 0)
                {
                    await RealTimeChart.AddValue(ToChartValue(experimentResult));
                }
            }
            AfterSimulation(cumulative / replicationCount);
            return cumulative / replicationCount;
        }
    }
}
*/
