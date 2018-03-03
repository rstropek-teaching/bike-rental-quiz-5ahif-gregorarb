﻿// <auto-generated />
using Bike_Rental_Service.Data;
using Bike_Rental_Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Bike_Rental_Service.Migrations
{
    [DbContext(typeof(BikeRentalContext))]
    partial class BikeRentalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bike_Rental_Service.Models.Bike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AdditionalHourPrice");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<int>("Category");

                    b.Property<double>("FirstHourPrice");

                    b.Property<DateTime?>("LastServiceDate");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("PurchaseDate")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Bikes");
                });

            modelBuilder.Entity("Bike_Rental_Service.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<int>("HouseNumber");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(75);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Town")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Bike_Rental_Service.Models.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<bool>("Paid");

                    b.Property<DateTime>("RentalBegin");

                    b.Property<DateTime?>("RentalEnd");

                    b.Property<int>("RentedBikeId");

                    b.Property<double>("TotalCosts");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RentedBikeId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("Bike_Rental_Service.Models.Rental", b =>
                {
                    b.HasOne("Bike_Rental_Service.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bike_Rental_Service.Models.Bike", "RentedBike")
                        .WithMany()
                        .HasForeignKey("RentedBikeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
