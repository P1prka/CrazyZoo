using CrazyZoo.Core;

namespace CrazyZoo.WpfApp;

public class CalculationItem
{
    public Animal Animal { get; }
    public bool IsSelected { get; set; }
    public string Text { get; }

    public CalculationItem(Animal animal, bool isSelected)
    {
        Animal = animal;
        IsSelected = isSelected;
        Text = $"{animal} | Energy: {animal.Energy}";
    }
}