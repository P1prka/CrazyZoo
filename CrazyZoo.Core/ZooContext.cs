using Microsoft.EntityFrameworkCore;

namespace CrazyZoo.Core;

public class ZooContext : DbContext
{
    public ZooContext(DbContextOptions<ZooContext> options)
        : base(options)
    {
    }

    public DbSet<AnimalRecord> Animals => Set<AnimalRecord>();

    public DbSet<GroupRecord> Groups => Set<GroupRecord>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<GroupRecord>().ToTable("AnimalGroups");
        model.Entity<GroupRecord>().HasKey(g => g.Id);
        model.Entity<GroupRecord>().Property(g => g.Name).HasMaxLength(50).IsRequired();
        model.Entity<GroupRecord>().HasIndex(g => g.Name).IsUnique();

        model.Entity<AnimalRecord>().ToTable("Animals");
        model.Entity<AnimalRecord>().HasKey(a => a.Id);
        model.Entity<AnimalRecord>().Property(a => a.Name).HasMaxLength(100).IsRequired();

        model.Entity<AnimalRecord>()
            .HasDiscriminator<string>("Kind")
            .HasValue<LionRecord>("Lion")
            .HasValue<ZebraRecord>("Zebra")
            .HasValue<GiraffeRecord>("Giraffe")
            .HasValue<HippoRecord>("Hippo")
            .HasValue<PenguinRecord>("Penguin")
            .HasValue<MonkeyRecord>("Monkey")
            .HasValue<FoxRecord>("Fox")
            .HasValue<PandaRecord>("Panda")
            .HasValue<ElephantRecord>("Elephant");

        model.Entity<GroupRecord>()
            .HasMany(g => g.Animals)
            .WithOne(a => a.Group)
            .HasForeignKey(a => a.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}