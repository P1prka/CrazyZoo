
namespace CrazyZoo.Core;

public class Elephant : Animal
{
    public Elephant(string name, int energy) : base(name, energy)
    {
    }

    // Слон гуляет по зоопарку
    public override string NormalAction()
    {
        if (!TryUseEnergy(5))
            return $"{Name} is too tired to walk.";

        return $"{Name} walks around the zoo.";
    }

    // Слон обливает всех водой
    public override string CrazyAction()
    {
        if (!TryUseEnergy(15))
            return $"{Name} is too tired to spray water.";

        return FinishCrazyAction(
            $"{Name} sprays water everywhere!");
    }

    // Реакция на другое животное
    protected override string GetCrazyReaction(Animal performer)
    {
        return $"{Name} trumpets for {performer.Name}!";
    }
}
