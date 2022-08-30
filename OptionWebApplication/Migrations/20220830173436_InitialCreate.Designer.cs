﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OptionWebApplication.Data;

#nullable disable

namespace OptionWebApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220830173436_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OptionWebApplication.Models.Assembly", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ChangeComponents")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CheckEngenire")
                        .HasColumnType("bit");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Component")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OtherWork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Party")
                        .HasColumnType("int");

                    b.Property<int>("People1")
                        .HasColumnType("int");

                    b.Property<int?>("People2")
                        .HasColumnType("int");

                    b.Property<int?>("People3")
                        .HasColumnType("int");

                    b.Property<int?>("People4")
                        .HasColumnType("int");

                    b.Property<int?>("People5")
                        .HasColumnType("int");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumberParty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Step1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Step2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Step3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Step4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Step5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeDevice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Assemblies");
                });

            modelBuilder.Entity("OptionWebApplication.Models.Guarentee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComplectedWork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Conclusion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOut")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiagnosticPeople")
                        .HasColumnType("int");

                    b.Property<string>("FaultDetection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RepairPeople")
                        .HasColumnType("int");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumberParty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeDevice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Guarentes");
                });
#pragma warning restore 612, 618
        }
    }
}