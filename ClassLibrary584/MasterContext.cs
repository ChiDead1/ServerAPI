using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary584;

public partial class MasterContext : DbContext
{
    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EasternNovelLibary>().ToTable("EasternNovelLibary");
        modelBuilder.Entity<EasternNovelLibary>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<EasternNovelLibary>()
            .Property(x => x.Id).IsRequired();
        modelBuilder.Entity<EasternNovelLibary>()
            .Property(x => x.Lat).HasColumnType("decimal(7,4)");
        modelBuilder.Entity<EasternNovelLibary>()
            .Property(x => x.Lon).HasColumnType("decimal(7,4)");

        modelBuilder.Entity<Novel>().ToTable("NovelLibary");
        modelBuilder.Entity<Novel>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Novel>()
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
    public DbSet<Novel> Novel => Set<Novel>();

}
