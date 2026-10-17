using System;

namespace CrazyZoo.Core;

public class AnimalEventArgs : EventArgs
{
    public Animal Animal { get; }

    public AnimalEventArgs(Animal animal)
    {
        ArgumentNullException.ThrowIfNull(animal);

        Animal = animal;
    }
}



