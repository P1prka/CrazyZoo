using System;
using System.Collections.Generic;

namespace CrazyZoo.Core;

public interface IRepository<T> where T : Animal
{
    void Add(T item);

    bool Remove(T item);

    IEnumerable<T> GetAll();

    T? Find(Func<T, bool> predicate);
    IEnumerable<T> SearchByName(string name);

    IEnumerable<T> SortByEnergy(bool descending);

    Dictionary<string, int> CountByEnergyLevel();

    double AverageEnergy();

}




