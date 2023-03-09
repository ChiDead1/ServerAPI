﻿// <auto-generated />
using System;
using ClassLibrary584;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClassLibrary584.Migrations
{
    [DbContext(typeof(WorldcitesContext))]
    [Migration("20230224201028_Popluations")]
    partial class Popluations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClassLibrary584.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CountryId")
                        .HasColumnType("int")
                        .HasColumnName("countryID");

                    b.Property<decimal>("Lat")
                        .HasColumnType("decimal(18, 4)")
                        .HasColumnName("lat");

                    b.Property<decimal>("Lon")
                        .HasColumnType("decimal(18, 0)")
                        .HasColumnName("lon");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("name")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("PK_cities_1");

                    b.HasIndex("CountryId");

                    b.ToTable("cities");
                });

            modelBuilder.Entity("ClassLibrary584.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Loc2")
                        .IsRequired()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("char(2)")
                        .HasColumnName("loc2")
                        .IsFixedLength();

                    b.Property<string>("Loc3")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("char(3)")
                        .HasColumnName("loc3")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("name")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("countries");
                });

            modelBuilder.Entity("ClassLibrary584.City", b =>
                {
                    b.HasOne("ClassLibrary584.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK_cities_countries");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("ClassLibrary584.Country", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
