using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyZoo.Core;

public class Zebra : Animal, IPerformer
{
    public Zebra(string name, int energy)
        : base(name, energy)
    {
    }

    public override string NormalAction()
    {
        if (!TryUseEnergy(5))
        {
            return $"{Name} is too tired to run.";
        }

        return $"{Name} runs around the enclosure.";
    }

    public string Perform()
    {
        if (!TryUseEnergy(10))
        {
            return $"{Name} is too tired to perform.";
        }

        return $"{Name} performs a very enthusiastic dance.";
    }

    public override string CrazyAction()
    {
        if (!TryUseEnergy(15))
        {
            return $"{Name} is too tired for an adventure.";
        }

        return $"{Name} suddenly plans a huge adventure outside the zoo.";
    }

}







