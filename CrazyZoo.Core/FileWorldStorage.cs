using System.IO;

namespace CrazyZoo.Core;

public class FileWorldStorage : IWorldStorage
{
    private readonly JsonWorldStorage json;
    private readonly XmlWorldStorage xml;

    public FileWorldStorage(JsonWorldStorage json, XmlWorldStorage xml)
    {
        this.json = json;
        this.xml = xml;
    }

    public Task SaveAsync(WorldDto world, string path,
        CancellationToken token = default)
    {
        return Pick(path).SaveAsync(world, path, token);
    }

    public Task<WorldDto> LoadAsync(string path,
        CancellationToken token = default)
    {
        return Pick(path).LoadAsync(path, token);
    }

    private IWorldStorage Pick(string path)
    {
        return Path.GetExtension(path).ToLowerInvariant() switch
        {
            ".json" => json,
            ".xml" => xml,
            _ => throw new NotSupportedException("Only .json and .xml files are supported.")
        };
    }
}



