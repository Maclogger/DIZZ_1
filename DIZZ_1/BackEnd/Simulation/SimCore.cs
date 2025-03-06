namespace DIZZ_1.BackEnd.Simulation;

public abstract class SimCore
{
    public double Run(int replicationCount)
    {
        BeforeSimulation();
        double cumulative = 0;
        for (int i = 0; i < replicationCount; i++)
        {
            BeforeSimulationRun();
            double experimentResult = RunExperiment();
            AfterSimulationRun(cumulative / (i + 1));
            cumulative += experimentResult;
        }

        AfterSimulation(cumulative / replicationCount);
        return cumulative / replicationCount;
    }

    public abstract void BeforeSimulation();
    public abstract void BeforeSimulationRun();
    public abstract void AfterSimulation(double solution);
    public abstract void AfterSimulationRun(double solution);

    public abstract double RunExperiment();
}