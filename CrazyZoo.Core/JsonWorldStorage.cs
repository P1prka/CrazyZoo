using System.IO;
using System.Text.Json;

namespace CrazyZoo.Core;

public class JsonWorldStorage : IWorldStorage
{
    private static readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true
    };

    public async Task SaveAsync(WorldDto world, string path,
        CancellationToken token = default)
    {
        await using FileStream stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, world, options, token);
    }

    public async Task<WorldDto> LoadAsync(string path,
        CancellationToken token = default)
    {
        await using FileStream stream = File.OpenRead(path);

        WorldDto? world = await JsonSerializer.DeserializeAsync<WorldDto>(
            stream, options, token);

        return world ?? throw new InvalidDataException("JSON file is empty.");
    }
}

