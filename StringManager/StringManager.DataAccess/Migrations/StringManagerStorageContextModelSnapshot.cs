﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StringManager.DataAccess;

namespace StringManager.DataAccess.Migrations
{
    [DbContext(typeof(StringManagerStorageContext))]
    partial class StringManagerStorageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StringManager.DataAccess.Entities.InstalledString", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MyInstrumentId")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int?>("StringId")
                        .HasColumnType("int");

                    b.Property<int?>("ToneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MyInstrumentId");

                    b.HasIndex("StringId");

                    b.HasIndex("ToneId");

                    b.ToTable("InstalledStrings");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfStrings")
                        .HasColumnType("int");

                    b.Property<int>("ScaleLenghtBass")
                        .HasColumnType("int");

                    b.Property<int>("ScaleLenghtTreble")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.MyInstrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GuitarPlace")
                        .HasColumnType("int");

                    b.Property<int>("HoursPlayedWeekly")
                        .HasColumnType("int");

                    b.Property<int?>("InstrumentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastDeepCleaning")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastStringChange")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NextDeepCleaning")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NextStringChange")
                        .HasColumnType("datetime2");

                    b.Property<string>("OwnName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId");

                    b.HasIndex("UserId");

                    b.ToTable("MyInstruments");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.String", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfDaysGood")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<double>("SpecificWeight")
                        .HasColumnType("float");

                    b.Property<int>("StringType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Strings");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.StringInSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int?>("StringId")
                        .HasColumnType("int");

                    b.Property<int?>("StringsSetId")
                        .HasColumnType("int");

                    b.Property<int?>("ToneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StringId");

                    b.HasIndex("StringsSetId");

                    b.HasIndex("ToneId");

                    b.ToTable("StringsInSets");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.StringsSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfStrings")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StringsSets");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.Tone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WaveLenght")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tones");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.ToneInTuning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int?>("ToneId")
                        .HasColumnType("int");

                    b.Property<int?>("TuningId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ToneId");

                    b.HasIndex("TuningId");

                    b.ToTable("ToneInTuning");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.Tuning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfStrings")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tunings");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<int>("DailyMaintanance")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayStyle")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.InstalledString", b =>
                {
                    b.HasOne("StringManager.DataAccess.Entities.MyInstrument", "MyInstrument")
                        .WithMany("InstalledStrings")
                        .HasForeignKey("MyInstrumentId");

                    b.HasOne("StringManager.DataAccess.Entities.String", "String")
                        .WithMany("InstalledStrings")
                        .HasForeignKey("StringId");

                    b.HasOne("StringManager.DataAccess.Entities.Tone", "Tone")
                        .WithMany()
                        .HasForeignKey("ToneId");

                    b.Navigation("MyInstrument");

                    b.Navigation("String");

                    b.Navigation("Tone");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.Instrument", b =>
                {
                    b.HasOne("StringManager.DataAccess.Entities.Manufacturer", "Manufacturer")
                        .WithMany("Instruments")
                        .HasForeignKey("ManufacturerId");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.MyInstrument", b =>
                {
                    b.HasOne("StringManager.DataAccess.Entities.Instrument", "Instrument")
                        .WithMany("MyInstruments")
                        .HasForeignKey("InstrumentId");

                    b.HasOne("StringManager.DataAccess.Entities.User", "User")
                        .WithMany("MyInstruments")
                        .HasForeignKey("UserId");

                    b.Navigation("Instrument");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.String", b =>
                {
                    b.HasOne("StringManager.DataAccess.Entities.Manufacturer", "Manufacturer")
                        .WithMany("Strings")
                        .HasForeignKey("ManufacturerId");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.StringInSet", b =>
                {
                    b.HasOne("StringManager.DataAccess.Entities.String", "String")
                        .WithMany("StringSets")
                        .HasForeignKey("StringId");

                    b.HasOne("StringManager.DataAccess.Entities.StringsSet", "StringsSet")
                        .WithMany("StringsInSet")
                        .HasForeignKey("StringsSetId");

                    b.HasOne("StringManager.DataAccess.Entities.Tone", null)
                        .WithMany("StringsInSets")
                        .HasForeignKey("ToneId");

                    b.Navigation("String");

                    b.Navigation("StringsSet");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.ToneInTuning", b =>
                {
                    b.HasOne("StringManager.DataAccess.Entities.Tone", "Tone")
                        .WithMany("TonesInTuning")
                        .HasForeignKey("ToneId");

                    b.HasOne("StringManager.DataAccess.Entities.Tuning", "Tuning")
                        .WithMany("TonesInTuning")
                        .HasForeignKey("TuningId");

                    b.Navigation("Tone");

                    b.Navigation("Tuning");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.Instrument", b =>
                {
                    b.Navigation("MyInstruments");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.Manufacturer", b =>
                {
                    b.Navigation("Instruments");

                    b.Navigation("Strings");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.MyInstrument", b =>
                {
                    b.Navigation("InstalledStrings");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.String", b =>
                {
                    b.Navigation("InstalledStrings");

                    b.Navigation("StringSets");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.StringsSet", b =>
                {
                    b.Navigation("StringsInSet");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.Tone", b =>
                {
                    b.Navigation("StringsInSets");

                    b.Navigation("TonesInTuning");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.Tuning", b =>
                {
                    b.Navigation("TonesInTuning");
                });

            modelBuilder.Entity("StringManager.DataAccess.Entities.User", b =>
                {
                    b.Navigation("MyInstruments");
                });
#pragma warning restore 612, 618
        }
    }
}
