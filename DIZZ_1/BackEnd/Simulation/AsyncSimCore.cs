using System;

namespace DIZZ_1.BackEnd.Simulation;

public class SimulationProgress<TProg>
{
    public int CurrentIteration { get; set; }
    public TProg Cumulative { get; set; } = default!;
}

public abstract class AsyncSimCore<TIter, TProg>
{
    public async Task<TProg> Run(
        int replicationCount,
        Func<TProg, TIter, TProg> aggregator,
        TProg initialValue,
        IProgress<SimulationProgress<TProg>> progress,
        CancellationToken cancellationToken = default
    )
    {
        return await Task.Run(async () =>
        {
            TProg cumulative = initialValue;
            await BeforeSimulation();
            try
            {
                for (int replication = 1; replication <= replicationCount; replication++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    await BeforeReplication(replication);
                    TIter experimentResult = await RunExperiment();
                    cumulative = aggregator(cumulative, experimentResult);

                    progress.Report(new SimulationProgress<TProg>
                    {
                        CurrentIteration = replication,
                        Cumulative = cumulative,
                    });

                    await AfterReplication(cumulative);
                }
            }
            catch (OperationCanceledException e)
            {
                await AfterSimulation(cumulative);
                return cumulative;
            }

            await AfterSimulation(cumulative);
            return cumulative;
        });
    }

    protected virtual Task BeforeSimulation() => Task.CompletedTask;
    protected virtual Task BeforeReplication(int replication) => Task.CompletedTask;
    protected virtual Task AfterSimulation(TProg cumulative) => Task.CompletedTask;
    protected virtual Task AfterReplication(TProg cumulative) => Task.CompletedTask;
    protected abstract Task<TIter> RunExperiment();
}