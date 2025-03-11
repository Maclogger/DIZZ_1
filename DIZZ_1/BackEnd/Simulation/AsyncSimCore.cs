using System;

namespace DIZZ_1.BackEnd.Simulation;

public class SimulationProgress<TProg>
{
    public int CurrentIteration { get; set; }
    public TProg Cumulative { get; set; } = default!;
}

public abstract class AsyncSimCore<TIter, TProg>
{
    private CancellationTokenSource? _cts;

    public async Task<(TProg, int)> Run(
        int replicationCount,
        Func<TProg, TIter, TProg> aggregator,
        TProg initialValue,
        IProgress<SimulationProgress<TProg>> progress
    )
    {
        _cts = new CancellationTokenSource();

        return await Task.Run(async () =>
        {
            TProg cumulative = initialValue;
            await BeforeSimulation();
            for (int replication = 1; replication <= replicationCount; replication++)
            {
                await BeforeReplication(replication);
                TIter experimentResult = await RunExperiment();
                cumulative = aggregator(cumulative, experimentResult);

                progress.Report(new SimulationProgress<TProg>
                {
                    CurrentIteration = replication,
                    Cumulative = cumulative,
                });

                await AfterReplication(cumulative, replication);

                if (_cts.IsCancellationRequested)
                {
                    await AfterSimulation(cumulative, replication);
                    return (cumulative, replication);
                }
            }

            await AfterSimulation(cumulative, replicationCount);
            return (cumulative, replicationCount);
        }, _cts.Token);
    }

// Add a method to request cancellation
    public void RequestCancellation()
    {
        _cts?.Cancel();
    }

    protected virtual Task BeforeSimulation() => Task.CompletedTask;
    protected virtual Task BeforeReplication(int replication) => Task.CompletedTask;
    protected virtual Task AfterSimulation(TProg cumulative, int replication) => Task.CompletedTask;
    protected virtual Task AfterReplication(TProg cumulative, int replication) => Task.CompletedTask;

    protected abstract Task<TIter>
        RunExperiment();
}