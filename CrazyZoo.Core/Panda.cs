
namespace CrazyZoo.Core;

public class Panda : Animal
{
    public Panda(string name, int energy) : base(name, energy)
    {
    }

    // Панда ест бамбук
    public override string NormalAction()
    {
        if (!TryUseEnergy(5))
            return $"{Name} is too tired to eat bamboo.";

        return $"{Name} eats bamboo.";
    }

    // Панда начинает кувыркаться
    public override string CrazyAction()
    {
        if (!TryUseEnergy(15))
            return $"{Name} is too tired to roll.";

        return FinishCrazyAction(
            $"{Name} rolls around the zoo!");
    }

    // Реакция на другое животное
    protected override string GetCrazyReaction(Animal performer)
    {
        return $"{Name} claps for {performer.Name}!";
    }
}
