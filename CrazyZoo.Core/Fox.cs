
namespace CrazyZoo.Core;

public class Fox : Animal
{
    public Fox(string name, int energy) : base(name, energy)
    {
    }

    // Лиса ищет еду
    public override string NormalAction()
    {
        if (!TryUseEnergy(5))
            return $"{Name} is too tired to look for food.";

        return $"{Name} looks for food.";
    }

    // Лиса начинает танцевать
    public override string CrazyAction()
    {
        if (!TryUseEnergy(15))
            return $"{Name} is too tired to dance.";

        return FinishCrazyAction(
            $"{Name} starts dancing around the zoo!");
    }

    // Реакция на другое животное
    protected override string GetCrazyReaction(Animal performer)
    {
        return $"{Name} jumps when {performer.Name} goes crazy!";
    }
}
