﻿// <auto-generated />
using ClassLibrary584;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClassLibrary584.Migrations
{
    [DbContext(typeof(MasterContext))]
    partial class MasterContextContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClassLibrary584.EasternNovelLibary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Lat")
                        .HasColumnType("decimal(7,4)");

                    b.Property<decimal>("Lon")
                        .HasColumnType("decimal(7,4)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NovelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Lat");

                    b.HasIndex("Lon");

                    b.HasIndex("Name");

                    b.HasIndex("NovelId");

                    b.ToTable("EasternNovelLibary", (string)null);
                });

            modelBuilder.Entity("ClassLibrary584.Novel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ISO2")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ISO3")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ISO2");

                    b.HasIndex("ISO3");

                    b.HasIndex("Name");

                    b.ToTable("NovelLibary", (string)null);
                });

            modelBuilder.Entity("ClassLibrary584.EasternNovelLibary", b =>
                {
                    b.HasOne("ClassLibrary584.Novel", "Novel")
                        .WithMany("EasternN")
                        .HasForeignKey("NovelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Novel");
                });

            modelBuilder.Entity("ClassLibrary584.Novel", b =>
                {
                    b.Navigation("EasternN");
                });
#pragma warning restore 612, 618
        }
    }
}
