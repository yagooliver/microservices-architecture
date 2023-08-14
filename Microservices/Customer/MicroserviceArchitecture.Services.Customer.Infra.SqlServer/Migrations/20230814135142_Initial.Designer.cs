﻿// <auto-generated />
using System;
using MicroserviceArchitecture.Services.Customer.Infra.SqlServer.ContextDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MicroserviceArchitecture.Services.Customer.Infra.SqlServer.Migrations
{
    [DbContext(typeof(CustomerDbContext))]
    [Migration("20230814135142_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MicroserviceArchitecture.Services.Customer.Model.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at")
                        .HasColumnOrder(5);

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("created_by")
                        .HasColumnOrder(6);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email")
                        .HasColumnOrder(4);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("first_name")
                        .HasColumnOrder(2);

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit")
                        .HasColumnName("is_removed")
                        .HasColumnOrder(7);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("last_name")
                        .HasColumnOrder(3);

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("removed_at")
                        .HasColumnOrder(8);

                    b.Property<Guid?>("RemovedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("removedBy")
                        .HasColumnOrder(9);

                    b.HasKey("Id");

                    b.ToTable("customer", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
