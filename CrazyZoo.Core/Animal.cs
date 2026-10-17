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


    internal event EventHandler<LogMessageEventArgs>? MessageCreated;

    internal event EventHandler<AnimalEventArgs>? CrazyActionFinished;

    internal void OnAnimalAdded(object? sender, AnimalEventArgs e)
    {
        if (ReferenceEquals(this, e.Animal))
        {
            return;
        }

        string message = $"{Name} welcomes {e.Animal.Name}!😊";

        MessageCreated?.Invoke(
            this,
            new LogMessageEventArgs(message));
    }

    internal void OnAnimalRemoved(object? sender, AnimalEventArgs e)
    {
        if (ReferenceEquals(this, e.Animal))
        {
            return;
        }

        string message = $"{Name} says goodbye to {e.Animal.Name}!👋";

        MessageCreated?.Invoke(
            this,
            new LogMessageEventArgs(message));
    }

    internal void OnOtherCrazyAction(object? sender, AnimalEventArgs e)
    {
        if (ReferenceEquals(this, e.Animal))
        {
            return;
        }

        if (!TryUseEnergy(2))
        {
            MessageCreated?.Invoke(
                this,
                new LogMessageEventArgs(
                    $"{Name} is too tired to react 🥲."));

            return;
        }

        string message = GetCrazyReaction(e.Animal);

        MessageCreated?.Invoke(
            this,
            new LogMessageEventArgs(message));
    }

    protected virtual string GetCrazyReaction(Animal performer)
    {
        return $"{Name} reacts to {performer.Name}'s crazy action!🎉";
    }

    protected string FinishCrazyAction(string message)
    {
        CrazyActionFinished?.Invoke(
            this,
            new AnimalEventArgs(this));

        return message;
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


















