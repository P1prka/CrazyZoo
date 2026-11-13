using System.IO;

namespace CrazyZoo.Core;

public static class WorldMapper
{
    public static WorldDto ToDto(IEnumerable<Animal> animals)
    {
        return new WorldDto
        {
            Version = 1,
            SavedAt = DateTime.Now,
            Animals = animals
                .Select(a => new AnimalDto
                {
                    Id = a.Id,
                    Type = a.GetType().Name,
                    Name = a.Name,
                    Energy = a.Energy,
                    Group = a.Group
                })
                .ToList()
        };
    }

    public static List<Animal> FromDto(WorldDto? world)
    {
        if (world?.Animals is null)
        {
            throw new InvalidDataException("The file does not contain world data.");
        }

        HashSet<Guid> ids = new();
        List<Animal> animals = new();

        foreach (AnimalDto? dto in world.Animals)
        {
            if (dto is null)
            {
                throw new InvalidDataException("Empty animal record.");
            }

            if (dto.Id == Guid.Empty || !ids.Add(dto.Id))
            {
                throw new InvalidDataException($"Invalid or duplicate id: {dto.Id}");
            }

            try
            {
                Animal animal = CreateAnimal(dto.Type, dto.Name, dto.Energy);
                animal.RestoreId(dto.Id);
                animal.MoveToGroup(dto.Group);
                animals.Add(animal);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidDataException($"Invalid animal '{dto.Name}': {ex.Message}", ex);
            }
        }

        return animals;
    }

    private static Animal CreateAnimal(string type, string name, int energy)
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
            _ => throw new InvalidDataException($"Unknown animal type: {type}")
        };
    }
}
