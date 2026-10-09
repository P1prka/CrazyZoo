using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyZoo.Core;

public class Lion : Animal, IPerformer
{
    public Lion(string name, int energy)
        : base(name, energy)
    {
    }

    public override string NormalAction()
    {
        if (!TryUseEnergy(5))
        {
            return $"{Name} is too tired to walk.";
        }

        return $"{Name} walks proudly around the zoo.";
    }

    public string Perform()
    {
        if (!TryUseEnergy(10))
        {
            return $"{Name} is too tired to perform.";
        }

        return $"{Name} performs a dramatic dance show.";
    }

    public override string CrazyAction()
    {
        if (!TryUseEnergy(15))
        {
            return $"{Name} is too tired for a crazy show.";
        }

        return $"{Name} decides that the whole zoo is now a stage.";
    }
}



