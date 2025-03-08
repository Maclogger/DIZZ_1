using System;

namespace DIZZ_1.BackEnd.Simulation
{
    public abstract class SimCore
    {
        public int CurrentReplication { get; set; } = 0;

        public double Run(int replicationCount)
        {
            BeforeSimulation();
            double cumulative = 0;
            for (CurrentReplication = 1;
                 CurrentReplication <= replicationCount;
                 CurrentReplication++)
            {
                BeforeReplication();
                double experimentResult = RunExperiment();
                cumulative += experimentResult;
                AfterReplication(cumulative / CurrentReplication);
            }

            AfterSimulation(cumulative / replicationCount);
            return cumulative / replicationCount;
        }

        protected abstract void BeforeSimulation();
        protected abstract void BeforeReplication();
        protected abstract void AfterSimulation(double solution);
        protected abstract void AfterReplication(double solution);
        protected abstract double RunExperiment();
    }
}