using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CrazyZoo.Core;

public class DesignFactory : IDesignTimeDbContextFactory<ZooContext>
{
    public ZooContext CreateDbContext(string[] args)
    {
        const string cs = @"Server=(localdb)\MSSQLLocalDB;Database=CrazyZooEfDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        DbContextOptions<ZooContext> options = new DbContextOptionsBuilder<ZooContext>()
            .UseSqlServer(cs)
            .Options;

        return new ZooContext(options);
    }
}