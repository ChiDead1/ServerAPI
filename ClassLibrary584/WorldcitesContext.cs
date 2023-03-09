using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary584;

public partial class WorldcitesContext : DbContext
{
    public WorldcitesContext()
    {
    }

    public WorldcitesContext(DbContextOptions<WorldcitesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=worldcitesGolden;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_cities_1");

            entity.Property(e => e.Name).IsFixedLength();

            entity.HasOne(d => d.Country).WithMany(p => p.Cities).HasConstraintName("FK_cities_countries");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Loc2).IsFixedLength();
            entity.Property(e => e.Loc3).IsFixedLength();
            entity.Property(e => e.Name).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
