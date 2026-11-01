using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyZoo.Core;

public static class CalculationRunner
{
    public static async Task<List<CalculationResult>> RunSequentialAsync(
        List<AnimalSnapshot> animals, int workload, CancellationToken token)
    {
        List<CalculationResult> results = new();

        foreach (AnimalSnapshot animal in animals)
        {
            CalculationResult result = await RunOneAsync(animal, workload, token);
            results.Add(result);
        }

        return results;
    }

    public static async Task<List<CalculationResult>> RunParallelAsync(
        List<AnimalSnapshot> animals, int workload, CancellationToken token)
    {
        List<Task<CalculationResult>> tasks = new();

        foreach (AnimalSnapshot animal in animals)
        {
            tasks.Add(RunOneAsync(animal, workload, token));
        }

        CalculationResult[] results = await Task.WhenAll(tasks);
        return results.ToList();
    }

    private static async Task<CalculationResult> RunOneAsync(
        AnimalSnapshot animal, int workload, CancellationToken token)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        try
        {
            double result = await Task.Run(
                () => AnimalSimulator.PredictEnergy(animal, workload, token), token);

            stopwatch.Stop();
            return new CalculationResult(animal.Name, "Completed",
                $"Predicted average energy: {result:0.00}", stopwatch.ElapsedMilliseconds);
        }
        catch (OperationCanceledException)
        {
            stopwatch.Stop();
            return new CalculationResult(animal.Name, "Cancelled",
                "Calculation was cancelled.", stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            return new CalculationResult(animal.Name, "Error",
                ex.Message, stopwatch.ElapsedMilliseconds);
        }
    }
}
