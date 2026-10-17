using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyZoo.Core;

public class Giraffe : Animal
{
    public Giraffe(string name, int energy)
        : base(name, energy)
    {
    }

    public override string NormalAction()
    {
        if (!TryUseEnergy(5))
        {
            return $"{Name} is too tired to eat leaves.";
        }

        return $"{Name} quietly eats some leaves.";
    }

    public override string CrazyAction()
    {
        if (!TryUseEnergy(10))
        {
            return $"{Name} is too tired for a crazy action.";
        }

        return FinishCrazyAction(
            $"{Name} discovers another completely imaginary illness.");
    }

}


