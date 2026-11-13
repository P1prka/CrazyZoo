namespace CrazyZoo.Core;

public class WorldDto
{
    public int Version { get; set; } = 1;

    public DateTime SavedAt { get; set; }

    public List<AnimalDto> Animals { get; set; } = new();
}

public class AnimalDto
{
    public Guid Id { get; set; }

    public string Type { get; set; } = "";

    public string Name { get; set; } = "";

    public int Energy { get; set; }

    public string Group { get; set; } = "";
}
