﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PerfectTunes.Models;

namespace PerfectTunes.Migrations
{
    [DbContext(typeof(PerfectTunesContext))]
    [Migration("20210425134800_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PerfectTunes.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("LogoImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductLine")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("BrandId");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            BrandId = 1,
                            BrandName = "Fender Stratocaster",
                            LogoImage = "Fender.jpg",
                            ProductLine = "Guitars"
                        },
                        new
                        {
                            BrandId = 2,
                            BrandName = "Yamaha",
                            LogoImage = "Yamaha.jpg",
                            ProductLine = "Orchestra"
                        },
                        new
                        {
                            BrandId = 3,
                            BrandName = "Ludwig",
                            LogoImage = "Ludwig.jpg",
                            ProductLine = "Drum Sets"
                        },
                        new
                        {
                            BrandId = 4,
                            BrandName = "Latin Percussion",
                            LogoImage = "LatinPercussion.jpg",
                            ProductLine = "Hand Percussion"
                        },
                        new
                        {
                            BrandId = 5,
                            BrandName = "Korg",
                            LogoImage = "Korg.jpg",
                            ProductLine = "Synthesizers"
                        },
                        new
                        {
                            BrandId = 6,
                            BrandName = "Jean Paul USA",
                            LogoImage = "JeanPaul.jpg",
                            ProductLine = "Band"
                        },
                        new
                        {
                            BrandId = 7,
                            BrandName = "Gemeinhardt",
                            LogoImage = "Gemeinhardt.jpg",
                            ProductLine = "Orchestra"
                        },
                        new
                        {
                            BrandId = 8,
                            BrandName = "Kaizer",
                            LogoImage = "Kaizer.jpg",
                            ProductLine = "Band"
                        });
                });

            modelBuilder.Entity("PerfectTunes.Models.Department", b =>
                {
                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            DepartmentId = "strings",
                            Name = "Strings"
                        },
                        new
                        {
                            DepartmentId = "percussion",
                            Name = "Percussion"
                        },
                        new
                        {
                            DepartmentId = "keys",
                            Name = "Keys"
                        },
                        new
                        {
                            DepartmentId = "brass",
                            Name = "Brass"
                        },
                        new
                        {
                            DepartmentId = "woodwinds",
                            Name = "Woodwinds"
                        },
                        new
                        {
                            DepartmentId = "gear",
                            Name = "Gear"
                        });
                });

            modelBuilder.Entity("PerfectTunes.Models.Instrument", b =>
                {
                    b.Property<int>("InstrumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LogoImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("InstrumentId");

                    b.HasIndex("BrandId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Instruments");

                    b.HasData(
                        new
                        {
                            InstrumentId = 1,
                            BrandId = 1,
                            DepartmentId = "strings",
                            LogoImage = "Squire.jpg",
                            Name = "Squier by StratoCaster",
                            Price = 219.99000000000001
                        },
                        new
                        {
                            InstrumentId = 2,
                            BrandId = 2,
                            DepartmentId = "strings",
                            LogoImage = "V3.jpg",
                            Name = "V3 Student Violin",
                            Price = 799.99000000000001
                        },
                        new
                        {
                            InstrumentId = 3,
                            BrandId = 3,
                            DepartmentId = "percussion",
                            LogoImage = "QuestLove.jpg",
                            Name = "Questlove Pocket Kit",
                            Price = 269.99000000000001
                        },
                        new
                        {
                            InstrumentId = 4,
                            BrandId = 4,
                            DepartmentId = "percussion",
                            LogoImage = "CitySeries.jpg",
                            Name = "City Series Conga",
                            Price = 349.99000000000001
                        },
                        new
                        {
                            InstrumentId = 5,
                            BrandId = 2,
                            DepartmentId = "keys",
                            LogoImage = "P-45.jpg",
                            Name = "P-45 88-Key Digital Piano",
                            Price = 499.99000000000001
                        },
                        new
                        {
                            InstrumentId = 6,
                            BrandId = 5,
                            DepartmentId = "keys",
                            LogoImage = "MicroKorg.jpg",
                            Name = "MicroKorg Synthesizer",
                            Price = 399.99000000000001
                        },
                        new
                        {
                            InstrumentId = 7,
                            BrandId = 6,
                            DepartmentId = "brass",
                            LogoImage = "AS-400.jpg",
                            Name = "AS-400 Alto Saxophone",
                            Price = 499.99000000000001
                        },
                        new
                        {
                            InstrumentId = 8,
                            BrandId = 7,
                            DepartmentId = "woodwinds",
                            LogoImage = "2SP.jpg",
                            Name = "2SP Student Flute",
                            Price = 479.99000000000001
                        },
                        new
                        {
                            InstrumentId = 9,
                            BrandId = 8,
                            DepartmentId = "brass",
                            LogoImage = "C-Series-3000.jpg",
                            Name = "C-Series (3000) Trumpet",
                            Price = 299.99000000000001
                        },
                        new
                        {
                            InstrumentId = 10,
                            BrandId = 6,
                            DepartmentId = "woodwinds",
                            LogoImage = "CL-300.jpg",
                            Name = "CL-300 Student Clarinet",
                            Price = 199.99000000000001
                        });
                });

            modelBuilder.Entity("PerfectTunes.Models.Instrument", b =>
                {
                    b.HasOne("PerfectTunes.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PerfectTunes.Models.Department", "Department")
                        .WithMany("Instruments")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
