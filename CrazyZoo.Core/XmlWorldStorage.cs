using System.IO;
using System.Xml.Serialization;

namespace CrazyZoo.Core;

public class XmlWorldStorage : IWorldStorage
{
    private static readonly XmlSerializer serializer = new(typeof(WorldDto));

    public Task SaveAsync(WorldDto world, string path,
        CancellationToken token = default)
    {
        return Task.Run(() =>
        {
            using FileStream stream = File.Create(path);
            serializer.Serialize(stream, world);
        }, token);
    }

    public Task<WorldDto> LoadAsync(string path,
        CancellationToken token = default)
    {
        return Task.Run(() =>
        {
            using FileStream stream = File.OpenRead(path);
            object? result = serializer.Deserialize(stream);

            return result as WorldDto
                ?? throw new InvalidDataException("XML file does not contain a world.");
        }, token);
    }
}

