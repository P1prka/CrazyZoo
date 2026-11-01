using System;
using System.Threading;

namespace CrazyZoo.Core;

public static class AnimalSimulator
{
    public const int MinWorkload = 1;
    public const int MaxWorkload = 50;

    public static double PredictEnergy(AnimalSnapshot animal, int workload, CancellationToken token)
    {
        if (workload < MinWorkload || workload > MaxWorkload)
        {
            throw new ArgumentOutOfRangeException(nameof(workload), "Workload must be between 1 and 50.");
        }

        if (animal.Energy == 0)
        {
            throw new InvalidOperationException($"{animal.Name} has 0 energy, prediction is not possible.");
        }

        int seed = 0;
        foreach (char c in animal.Name)
        {
            seed += c;
        }

        Random random = new Random(seed);
        long rounds = workload * 20_000_000L;
        double energy = animal.Energy;
        double total = 0;

        for (long i = 0; i < rounds; i++)
        {
            token.ThrowIfCancellationRequested();

            energy += random.Next(-3, 4);

            if (energy < 0) energy = 0;
            if (energy > 100) energy = 100;

            total += energy;
        }

        return total / rounds;
    }
}
