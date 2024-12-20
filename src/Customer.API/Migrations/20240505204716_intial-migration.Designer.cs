﻿// <auto-generated />
using System;
using CustomerMan.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomerMan.API.Migrations
{
    [DbContext(typeof(CustomerManDbContext))]
    [Migration("20240505204716_intial-migration")]
    partial class Intialmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("customerContext")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomerMan.Domain.AggregateModels.CustomerAggregate.Customer", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Customers", "customerContext");
                });

            modelBuilder.Entity("CustomerMan.Domain.AggregateModels.CustomerAggregate.Customer", b =>
                {
                    b.OwnsOne("CustomerMan.Domain.AggregateModels.CustomerAggregate.Adress", "Adress", b1 =>
                        {
                            b1.Property<Guid>("CustomerID")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasColumnType("nvarchar(max)")
                                .HasDefaultValue("")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasColumnType("nvarchar(max)")
                                .HasDefaultValue("")
                                .HasColumnName("Country");

                            b1.Property<string>("State")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasColumnType("nvarchar(max)")
                                .HasDefaultValue("")
                                .HasColumnName("State");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasColumnType("nvarchar(max)")
                                .HasDefaultValue("")
                                .HasColumnName("Street");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasColumnType("nvarchar(max)")
                                .HasDefaultValue("")
                                .HasColumnName("ZipCode");

                            b1.HasKey("CustomerID");

                            b1.ToTable("Customers", "customerContext");

                            b1.WithOwner()
                                .HasForeignKey("CustomerID");
                        });

                    b.OwnsOne("SharedKernel.Models.AudityInfo", "AudityInfo", b1 =>
                        {
                            b1.Property<Guid>("CustomerID")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("CreatedBy");

                            b1.Property<DateTime>("CreatedDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("CreatedDate");

                            b1.Property<Guid>("DeletedBy")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("DeletedBy");

                            b1.Property<DateTime>("DeletedDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("DeletedDate");

                            b1.Property<Guid>("ModifiedBy")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("ModifiedBy");

                            b1.Property<DateTime>("ModifiedDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("ModifiedDate");

                            b1.Property<Guid>("RestoredBy")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("RestoredBy");

                            b1.Property<DateTime>("RestoredDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("RestoredDate");

                            b1.HasKey("CustomerID");

                            b1.ToTable("Customers", "customerContext");

                            b1.WithOwner()
                                .HasForeignKey("CustomerID");
                        });

                    b.Navigation("Adress")
                        .IsRequired();

                    b.Navigation("AudityInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
