﻿// <auto-generated />
using System;
using Insurance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Insurance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230419150515_Double")]
    partial class Double
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Insurance.Models.InsuranceRecord", b =>
                {
                    b.Property<int>("InsuranceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InsuranceId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("InsuranceRecordName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsuredSince")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InsuredUntil")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InsuranceId");

                    b.ToTable("InsuranceRecords");
                });

            modelBuilder.Entity("Insurance.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Insurance.Models.UserInsurance", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("InsuranceId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "InsuranceId");

                    b.HasIndex("InsuranceId");

                    b.ToTable("UserInsurance");
                });

            modelBuilder.Entity("Insurance.Models.UserInsurance", b =>
                {
                    b.HasOne("Insurance.Models.InsuranceRecord", "InsuranceRecord")
                        .WithMany("UserInsuranceRecords")
                        .HasForeignKey("InsuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Insurance.Models.User", "User")
                        .WithMany("UserInsuranceRecords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsuranceRecord");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Insurance.Models.InsuranceRecord", b =>
                {
                    b.Navigation("UserInsuranceRecords");
                });

            modelBuilder.Entity("Insurance.Models.User", b =>
                {
                    b.Navigation("UserInsuranceRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
