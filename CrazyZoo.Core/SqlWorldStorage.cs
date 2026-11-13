using Microsoft.Data.SqlClient;

namespace CrazyZoo.Core;
public class SqlWorldStorage : IWorldStorage
{
    private const string CreateTablesSql = @"
IF OBJECT_ID(N'dbo.AnimalGroups', N'U') IS NULL
CREATE TABLE dbo.AnimalGroups (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL UNIQUE
);

IF OBJECT_ID(N'dbo.Animals', N'U') IS NULL
CREATE TABLE dbo.Animals (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Type NVARCHAR(50) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Energy INT NOT NULL CHECK (Energy BETWEEN 0 AND 100),
    GroupId INT NOT NULL REFERENCES dbo.AnimalGroups(Id)
);";

    private readonly string connectionString;

    public SqlWorldStorage(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task SaveAsync(WorldDto world, string path,
        CancellationToken token = default)
    {
        await EnsureDatabaseAsync(token);

        await using SqlConnection conn = new(connectionString);
        await conn.OpenAsync(token);

        using SqlTransaction tx = conn.BeginTransaction();

        try
        {
            await using (SqlCommand clear = new(
                "DELETE FROM dbo.Animals; DELETE FROM dbo.AnimalGroups;", conn, tx))
            {
                await clear.ExecuteNonQueryAsync(token);
            }

            Dictionary<string, int> groupIds = new();

            foreach (string group in world.Animals.Select(a => a.Group).Distinct())
            {
                await using SqlCommand insertGroup = new(
                    "INSERT INTO dbo.AnimalGroups (Name) OUTPUT INSERTED.Id VALUES (@name);",
                    conn, tx);
                insertGroup.Parameters.AddWithValue("@name", group);

                object? id = await insertGroup.ExecuteScalarAsync(token);
                groupIds[group] = Convert.ToInt32(id);
            }

            foreach (AnimalDto animal in world.Animals)
            {
                await using SqlCommand insertAnimal = new(
                    "INSERT INTO dbo.Animals (Id, Type, Name, Energy, GroupId) " +
                    "VALUES (@id, @type, @name, @energy, @groupId);",
                    conn, tx);
                insertAnimal.Parameters.AddWithValue("@id", animal.Id);
                insertAnimal.Parameters.AddWithValue("@type", animal.Type);
                insertAnimal.Parameters.AddWithValue("@name", animal.Name);
                insertAnimal.Parameters.AddWithValue("@energy", animal.Energy);
                insertAnimal.Parameters.AddWithValue("@groupId", groupIds[animal.Group]);

                await insertAnimal.ExecuteNonQueryAsync(token);
            }

            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public async Task<WorldDto> LoadAsync(string path,
        CancellationToken token = default)
    {
        await EnsureDatabaseAsync(token);

        await using SqlConnection conn = new(connectionString);
        await conn.OpenAsync(token);

        await using SqlCommand cmd = new(
            "SELECT a.Id, a.Type, a.Name, a.Energy, g.Name " +
            "FROM dbo.Animals a JOIN dbo.AnimalGroups g ON g.Id = a.GroupId " +
            "ORDER BY a.Type, a.Name;", conn);

        await using SqlDataReader r = await cmd.ExecuteReaderAsync(token);

        WorldDto world = new();

        while (await r.ReadAsync(token))
        {
            world.Animals.Add(new AnimalDto
            {
                Id = r.GetGuid(0),
                Type = r.GetString(1),
                Name = r.GetString(2),
                Energy = r.GetInt32(3),
                Group = r.GetString(4)
            });
        }

        return world;
    }

    private async Task EnsureDatabaseAsync(CancellationToken token)
    {
        SqlConnectionStringBuilder builder = new(connectionString);
        string databaseName = builder.InitialCatalog;
        builder.InitialCatalog = "master";

        await using (SqlConnection master = new(builder.ConnectionString))
        {
            await master.OpenAsync(token);

            await using SqlCommand create = new(
                "IF DB_ID(@name) IS NULL " +
                "BEGIN " +
                "DECLARE @sql NVARCHAR(400) = N'CREATE DATABASE ' + QUOTENAME(@name); " +
                "EXEC(@sql); " +
                "END", master);
            create.Parameters.AddWithValue("@name", databaseName);

            await create.ExecuteNonQueryAsync(token);
        }

        await using SqlConnection conn = new(connectionString);
        await conn.OpenAsync(token);

        await using SqlCommand tables = new(CreateTablesSql, conn);
        await tables.ExecuteNonQueryAsync(token);
    }
}

