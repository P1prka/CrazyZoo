using System;
using System.Collections.Generic;
using System.Text;


namespace CrazyZoo.Core;

public class Penguin : Animal, ISwimmable
{
    public Penguin(string name, int energy)
        : base(name, energy)
    {
    }

    public override string NormalAction()
    {
        if (!TryUseEnergy(5))
        {
            return $"{Name} is too tired to walk.";
        }

        return $"{Name} walks around with a very serious expression.";
    }

    public string Swim()
    {
        if (!TryUseEnergy(10))
        {
            return $"{Name} is too tired to swim.";
        }

        return $"{Name} swims quickly through the water.";
    }

    public override string CrazyAction()
    {
        if (!TryUseEnergy(15))
        {
            return $"{Name} is too tired for a secret operation.";
        }

        return FinishCrazyAction(
            $"{Name} starts a top-secret zoo operation.");
    }
}


