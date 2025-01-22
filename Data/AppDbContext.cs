using Microsoft.EntityFrameworkCore;
using Person.Model;

public class AppDbContext : DbContext
{
    public DbSet<PersonModel> Pessoas { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PersonModel>(entity =>
        {
            entity.ToTable("pessoas");
            entity.HasKey(p => p.Codigo);
            entity.Property(p => p.Nome).IsRequired();
            entity.Property(p => p.Cidade).HasMaxLength(100);
            entity.Property(p => p.Idade).IsRequired();
        });
    }
}
