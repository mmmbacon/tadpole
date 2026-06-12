using Microsoft.EntityFrameworkCore;
using Tadpole.Domain.Entities;

namespace Tadpole.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Guardian> Guardians => Set<Guardian>();
    public DbSet<Child> Children => Set<Child>();
    public DbSet<Connection> Connections => Set<Connection>();
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Guardian>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Email).IsRequired();
            b.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Child>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasOne(x => x.Guardian)
                .WithMany(g => g.Children)
                .HasForeignKey(x => x.GuardianId);
            b.HasIndex(x => x.Username).IsUnique();
            b.Property(x => x.FriendCode).HasMaxLength(8);
        });

        modelBuilder.Entity<Connection>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => new { x.ChildLoId, x.ChildHiId }).IsUnique();
            b.ToTable(t => t.HasCheckConstraint(
                "CK_connections_child_lo_lt_hi",
                "\"ChildLoId\" < \"ChildHiId\""));
        });

        modelBuilder.Entity<Message>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Body).HasMaxLength(2000);
        });
    }
}
