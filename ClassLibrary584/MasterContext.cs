using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClassLibrary584;

public partial class MasterContext : IdentityDbContext<ApplicationUser>
{
    public MasterContext(): base()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=comp584.ce717m7yn2n9.us-east-2.rds.amazonaws.com;Initial Catalog=NovelLibary;User ID=nhan;password=User@#123;TrustServerCertificate = True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EasternNovelLibary>().ToTable("EasternNovelLibary");
        modelBuilder.Entity<EasternNovelLibary>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<EasternNovelLibary>()
            .Property(x => x.Id).IsRequired();
        modelBuilder.Entity<EasternNovelLibary>()
            .Property(x => x.Chapter).HasColumnType("decimal(7,4)");
        modelBuilder.Entity<EasternNovelLibary>()
        .Property(x => x.Author).HasColumnType("decimal(7,4)");

        modelBuilder.Entity<NovelLibary>().ToTable("NovelLibary");
        modelBuilder.Entity<NovelLibary>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<NovelLibary>()
            .Property(x => x.Id).IsRequired();
        modelBuilder.Entity<EasternNovelLibary>()
            .HasOne(x => x.Novel)
            .WithMany(y => y.EasternN)
            .HasForeignKey(x => x.CountryId);

        // add the EntityTypeConfiguration classes
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(MasterContext).Assembly
            );
    }

    public DbSet<EasternNovelLibary> EasternNovelLibary => Set<EasternNovelLibary>();
    public DbSet<NovelLibary> NovelL => Set<NovelLibary>();

}   
