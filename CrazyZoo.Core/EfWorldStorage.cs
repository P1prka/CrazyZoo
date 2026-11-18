using Microsoft.EntityFrameworkCore;

namespace CrazyZoo.Core;

public class EfWorldStorage : IWorldStorage
{
    private readonly IDbContextFactory<ZooContext> factory;

    public EfWorldStorage(IDbContextFactory<ZooContext> factory)
    {
        this.factory = factory;
    }

    public async Task SaveAsync(WorldDto world, string path,
        CancellationToken token = default)
    {
        await using ZooContext db = await factory.CreateDbContextAsync(token);
        await using var tx = await db.Database.BeginTransactionAsync(token);

        List<AnimalRecord> oldAnimals = await db.Animals.ToListAsync(token);
        db.Animals.RemoveRange(oldAnimals);
        await db.SaveChangesAsync(token);

        Dictionary<string, GroupRecord> groups =
            await db.Groups.ToDictionaryAsync(g => g.Name, token);

        foreach (AnimalDto dto in world.Animals)
        {
            if (!groups.TryGetValue(dto.Group, out GroupRecord? group))
            {
                group = new GroupRecord { Name = dto.Group };
                db.Groups.Add(group);
                groups[dto.Group] = group;
            }

            AnimalRecord record = CreateRecord(dto.Type);
            record.Id = dto.Id;
            record.Name = dto.Name;
            record.Energy = dto.Energy;
            record.Group = group;

            db.Animals.Add(record);
        }

        await db.SaveChangesAsync(token);
        await tx.CommitAsync(token);
    }

    public async Task<WorldDto> LoadAsync(string path,
        CancellationToken token = default)
    {
        await using ZooContext db = await factory.CreateDbContextAsync(token);

        List<AnimalRecord> records = await db.Animals
            .AsNoTracking()
            .Include(a => a.Group)
            .OrderBy(a => a.Name)
            .ToListAsync(token);

        WorldDto world = new();

        foreach (AnimalRecord record in records)
        {
            world.Animals.Add(new AnimalDto
            {
                Id = record.Id,
                Type = record.GetType().Name.Replace("Record", ""),
                Name = record.Name,
                Energy = record.Energy,
                Group = record.Group?.Name ?? ""
            });
        }

        return world;
    }

    private static AnimalRecord CreateRecord(string type)
    {
        return type switch
        {
            "Lion" => new LionRecord(),
            "Zebra" => new ZebraRecord(),
            "Giraffe" => new GiraffeRecord(),
            "Hippo" => new HippoRecord(),
            "Penguin" => new PenguinRecord(),
            "Monkey" => new MonkeyRecord(),
            "Fox" => new FoxRecord(),
            "Panda" => new PandaRecord(),
            "Elephant" => new ElephantRecord(),
            _ => throw new InvalidDataException($"Unknown animal type: {type}")
        };
    }
}