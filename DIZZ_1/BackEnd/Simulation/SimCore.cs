namespace DIZZ_1.BackEnd.Simulation;

public abstract class SimCore
{
    public int CurrentRun { get; set; } = 0;

    public double Run(int replicationCount)
    {
        CurrentRun = 0;
        BeforeSimulation();
        double cumulative = 0;
        for (int i = 0; i < replicationCount; i++)
        {
            BeforeSimulationRun();
            double experimentResult = RunExperiment();
            AfterSimulationRun(cumulative);
            cumulative += experimentResult;
            CurrentRun++;
        }

        AfterSimulation(cumulative);
        return cumulative / replicationCount;
    }

    public abstract void BeforeSimulation();
    public abstract void BeforeSimulationRun();
    public abstract void AfterSimulation(double cumulative);
    public abstract void AfterSimulationRun(double cumulative);

    public abstract double RunExperiment();
}