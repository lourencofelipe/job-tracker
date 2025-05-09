namespace JobTracker.Infrastructure.Context;
public class EfContext : DbContext
{
    public DbSet<JobApplication> JobApplications { get; set; }

    public EfContext(DbContextOptions<EfContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobApplication>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<JobApplication>()
            .Property(p => p.CompanyName)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<JobApplication>()
            .Property(p => p.Position)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<JobApplication>()
            .Property(p => p.Status)
            .IsRequired()
            .HasMaxLength(10);

        modelBuilder.Entity<JobApplication>()
            .Property(p => p.DateApplied)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
