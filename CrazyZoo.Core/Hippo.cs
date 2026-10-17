using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyZoo.Core;

public class Hippo : Animal, ISwimmable, IPerformer
{
    public Hippo(string name, int energy)
        : base(name, energy)
    {
    }

    public override string NormalAction()
    {
        if (!TryUseEnergy(5))
        {
            return $"{Name} is too tired.";
        }

        return $"{Name} relaxes near the water.";
    }

    public string Swim()
    {
        if (!TryUseEnergy(10))
        {
            return $"{Name} is too tired to swim.";
        }

        return $"{Name} swims across the pool.";
    }

    public string Perform()
    {
        if (!TryUseEnergy(10))
        {
            return $"{Name} is too tired to perform.";
        }

        return $"{Name} performs an impressive dance.";
    }

    public override string CrazyAction()
    {
        if (!TryUseEnergy(20))
        {
            return $"{Name} is too tired for a crazy action.";
        }

        return FinishCrazyAction(
            $"{Name} jumps into the pool and splashes everyone nearby.");
    }
}