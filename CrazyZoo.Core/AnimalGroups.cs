namespace CrazyZoo.Core;

public static class AnimalGroups
{



    public const string Default = "Savanna";

    public static readonly string[] All =
    {
        "Savanna", "Jungle", "Water", "Arctic"
    };

    public static bool IsValid(string? group)
    {
        return group is not null && All.Contains(group);
    }
}







