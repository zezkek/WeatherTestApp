﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherTestApp.Data;

#nullable disable

namespace WeatherTestApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230511182626_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WeatherTestApp.Models.WeatherPeriod", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CloudBase")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cloudiness")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dew")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HorizontalVisibility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Humidity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pressure")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Temperature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WeatherConditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WindDirection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WindSpeed")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("WeatherPeriods");
                });
#pragma warning restore 612, 618
        }
    }
}
