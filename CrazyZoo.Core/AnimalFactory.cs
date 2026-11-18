namespace CrazyZoo.Core;

public static class AnimalFactory
{
    public static readonly string[] Types =
    {
        "Lion", "Zebra", "Giraffe",
        "Hippo", "Penguin", "Monkey",
        "Fox", "Panda", "Elephant"
    };

    public static Animal Create(string type, string name, int energy)
    {
        return type switch
        {
            "Lion" => new Lion(name, energy),
            "Zebra" => new Zebra(name, energy),
            "Giraffe" => new Giraffe(name, energy),
            "Hippo" => new Hippo(name, energy),
            "Penguin" => new Penguin(name, energy),
            "Monkey" => new Monkey(name, energy),
            "Fox" => new Fox(name, energy),
            "Panda" => new Panda(name, energy),
            "Elephant" => new Elephant(name, energy),
            _ => throw new ArgumentException($"Unknown animal type: {type}")
        };
    }
}