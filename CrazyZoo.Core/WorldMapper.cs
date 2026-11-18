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
                Animal animal = AnimalFactory.Create(dto.Type, dto.Name, dto.Energy);
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
}