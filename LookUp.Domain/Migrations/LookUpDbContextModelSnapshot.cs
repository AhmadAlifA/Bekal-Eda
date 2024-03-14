﻿// <auto-generated />
using System;
using LookUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LookUp.Domain.Migrations
{
    [DbContext(typeof(LookUpDbContext))]
    partial class LookUpDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LookUp.Domain.Entities.AttributeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Unit")
                        .IsUnique();

                    b.ToTable("Attributes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("82d70ed1-23f7-4065-a141-7b2d0e2cf341"),
                            Status = 0,
                            Type = 0,
                            Unit = "Unit"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
