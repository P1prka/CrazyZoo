
namespace CrazyZoo.Core;

public class Monkey : Animal, IPerformer
{
    public Monkey(string name, int energy)
        : base(name, energy)
    {
    }

    public override string NormalAction()
    {
        if (!TryUseEnergy(5))
        {
            return $"{Name} is too tired to eat a banana.";
        }

        return $"{Name} happily eats a banana.";
    }

    public override string CrazyAction()
    {
        if (!TryUseEnergy(10))
        {
            return $"{Name} is too tired for a crazy action.";
        }

        return $"{Name} throws a banana peel at the visitors!";
    }

    public string Perform()
    {
        if (!TryUseEnergy(5))
        {
            return $"{Name} is too tired to perform.";
        }

        return $"{Name} dances and makes funny faces!";
    }
}
