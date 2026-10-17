using System;
using System.Collections.Generic;
using System.Linq;
namespace CrazyZoo.Core;

public class Repository<T> : IRepository<T> where T : Animal
{
    private readonly List<T> items = new();

    public event EventHandler<AnimalEventArgs>? AnimalAdded;

    public event EventHandler<AnimalEventArgs>? AnimalRemoved;
    public event EventHandler<LogMessageEventArgs>? LogCreated;

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (items.Contains(item))
        {
            throw new ArgumentException(
                "Animal is already in the repository.");
        }


        items.Add(item);

        item.MessageCreated += OnAnimalMessage;

        AnimalAdded += item.OnAnimalAdded;
        AnimalRemoved += item.OnAnimalRemoved;
        item.CrazyActionFinished += OnCrazyActionFinished;

        AnimalAdded?.Invoke(
            this,
            new AnimalEventArgs(item));

    }



    public bool Remove(T item)
    {
        ArgumentNullException.ThrowIfNull(item);

        bool removed = items.Remove(item);

        if (!removed)
        {
            return false;
        }

        AnimalAdded -= item.OnAnimalAdded;
        AnimalRemoved -= item.OnAnimalRemoved;
        item.CrazyActionFinished -= OnCrazyActionFinished;
        item.MessageCreated -= OnAnimalMessage;

        AnimalRemoved?.Invoke(
            this,
            new AnimalEventArgs(item));

        return true;
    }



    public IEnumerable<T> GetAll()
    {
        return items.AsReadOnly();
    }

    public T? Find(Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        foreach (T item in items)
        {
            if (predicate(item))
            {
                return item;
            }
        }

        return null;
    }



    public IEnumerable<T> SearchByName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        return items
            .Where(animal => animal.Name.Contains(
                name.Trim(),
                StringComparison.OrdinalIgnoreCase))
            .ToList();
    }




    public IEnumerable<T> SortByEnergy(bool descending)
    {
        if (descending)
        {
            return items
                .OrderByDescending(animal => animal.Energy)
                .ToList();
        }

        return items
            .OrderBy(animal => animal.Energy)
            .ToList();
    }




    public Dictionary<string, int> CountByEnergyLevel()
    {
        return items
            .GroupBy(animal =>
                animal.Energy < 30 ? "Low" :
                animal.Energy < 70 ? "Medium" : "High")
            .ToDictionary(
                group => group.Key,
                group => group.Count());
    }

    public double AverageEnergy()
    {
        if (items.Count == 0)
        {
            return 0;
        }

        return items.Average(animal => animal.Energy);
    }



    private void OnAnimalMessage(
    object? sender,
    LogMessageEventArgs e)
    {
        LogCreated?.Invoke(this, e);
    }



    private void OnCrazyActionFinished(
        object? sender,
        AnimalEventArgs e)
    {
        foreach (T animal in items)
        {
            animal.OnOtherCrazyAction(sender, e);
        }
    }

}




