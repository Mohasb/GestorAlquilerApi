﻿// <auto-generated />
using System;
using GestorAlquilerApi.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestorAlquilerApi.DataAccessLayer.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("GestorAlquilerApi.BussinessLogicLayer.Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cif")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Population")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Cif")
                        .IsUnique();

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("GestorAlquilerApi.BussinessLogicLayer.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BranchId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("FuelType")
                        .HasColumnType("TEXT");

                    b.Property<string>("GearShiftType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("Registration")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("Registration")
                        .IsUnique();

                    b.ToTable("Car");
                });

            modelBuilder.Entity("GestorAlquilerApi.BussinessLogicLayer.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BankAccount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Registration")
                        .HasColumnType("TEXT");

                    b.Property<string>("Rol")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Registration")
                        .IsUnique();

                    b.ToTable("Client");
                });

            modelBuilder.Entity("GestorAlquilerApi.BussinessLogicLayer.Models.Planning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BranchId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarCategory")
                        .HasColumnType("TEXT");

                    b.Property<int>("CarsAvailables")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Day")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Planning");
                });

            modelBuilder.Entity("GestorAlquilerApi.BussinessLogicLayer.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BranchId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarCategory")
                        .HasColumnType("TEXT");

                    b.Property<int>("CarId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ReturnBranchId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("CarId");

                    b.HasIndex("ClientId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("GestorAlquilerApi.BussinessLogicLayer.Models.Car", b =>
                {
                    b.HasOne("GestorAlquilerApi.BussinessLogicLayer.Models.Branch", "Branch")
                        .WithMany("Cars")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("GestorAlquilerApi.BussinessLogicLayer.Models.Planning", b =>
                {
                    b.HasOne("GestorAlquilerApi.BussinessLogicLayer.Models.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("GestorAlquilerApi.BussinessLogicLayer.Models.Reservation", b =>
                {
                    b.HasOne("GestorAlquilerApi.BussinessLogicLayer.Models.Branch", "Branch")
                        .WithMany("Reservations")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestorAlquilerApi.BussinessLogicLayer.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestorAlquilerApi.BussinessLogicLayer.Models.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Car");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("GestorAlquilerApi.BussinessLogicLayer.Models.Branch", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("GestorAlquilerApi.BussinessLogicLayer.Models.Client", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
