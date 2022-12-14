// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SpecificationPattern.Infra.Repositories;

#nullable disable

namespace SpecificationPattern.Infra.Migration.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SpecificationPattern.Domain.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("SpecificationPattern.Domain.Entities.Grape", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Color")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("WineId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WineId");

                    b.ToTable("Grape");
                });

            modelBuilder.Entity("SpecificationPattern.Domain.Entities.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("SpecificationPattern.Domain.Entities.Wine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WineryId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.HasIndex("WineryId");

                    b.ToTable("Wines");
                });

            modelBuilder.Entity("SpecificationPattern.Domain.Entities.Winery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Winery");
                });

            modelBuilder.Entity("SpecificationPattern.Domain.Entities.Grape", b =>
                {
                    b.HasOne("SpecificationPattern.Domain.Entities.Wine", null)
                        .WithMany("Grapes")
                        .HasForeignKey("WineId");
                });

            modelBuilder.Entity("SpecificationPattern.Domain.Entities.Region", b =>
                {
                    b.HasOne("SpecificationPattern.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("SpecificationPattern.Domain.Entities.Wine", b =>
                {
                    b.HasOne("SpecificationPattern.Domain.Entities.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpecificationPattern.Domain.Entities.Winery", "Winery")
                        .WithMany()
                        .HasForeignKey("WineryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");

                    b.Navigation("Winery");
                });

            modelBuilder.Entity("SpecificationPattern.Domain.Entities.Wine", b =>
                {
                    b.Navigation("Grapes");
                });
#pragma warning restore 612, 618
        }
    }
}
