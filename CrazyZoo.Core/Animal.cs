using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyZoo.Core;


public abstract class Animal
{
    public string Name { get; }

    public int Energy { get; private set; }

    protected Animal(string name, int energy)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.");
        }

        if (energy < 0 || energy > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(energy),
                "Energy must be between 0 and 100.");
        }

        Name = name.Trim();
        Energy = energy;
    }

    public abstract string NormalAction();

    public abstract string CrazyAction();

    protected void ChangeEnergy(int amount)
    {
        int newEnergy = Energy + amount;

        if (newEnergy < 0)
        {
            newEnergy = 0;
        }

        if (newEnergy > 100)
        {
            newEnergy = 100;
        }

        Energy = newEnergy;
    }

    public override string ToString()
    {
        return $"{GetType().Name}: {Name}";
    }



    protected bool TryUseEnergy(int amount)
    {
        if (Energy < amount)
        {
            return false;
        }

        Energy -= amount;
        return true;
    }





}


















