﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersistenceService.Persistence;

#nullable disable

namespace PersistenceService.Migrations
{
    [DbContext(typeof(InverterDataContext))]
    [Migration("20250204071512_AddStatusToInverterData")]
    partial class AddStatusToInverterData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PersistenceService.Models.InverterData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("AC_output_active_power")
                        .HasColumnType("real");

                    b.Property<float>("AC_output_apparent_power")
                        .HasColumnType("real");

                    b.Property<float>("AC_output_frequency")
                        .HasColumnType("real");

                    b.Property<float>("AC_output_voltage")
                        .HasColumnType("real");

                    b.Property<float>("Battery_capacity")
                        .HasColumnType("real");

                    b.Property<float>("Battery_charging_current")
                        .HasColumnType("real");

                    b.Property<float>("Battery_discharge_current")
                        .HasColumnType("real");

                    b.Property<float>("Battery_voltage")
                        .HasColumnType("real");

                    b.Property<float>("Battery_voltage_from_SCC")
                        .HasColumnType("real");

                    b.Property<float>("Bus_voltage")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Grid_frequency")
                        .HasColumnType("real");

                    b.Property<float>("Grid_voltage")
                        .HasColumnType("real");

                    b.Property<float>("Inverter_heatsink_temperature")
                        .HasColumnType("real");

                    b.Property<float>("Output_Load_Percent")
                        .HasColumnType("real");

                    b.Property<float>("PV_Input_Voltage")
                        .HasColumnType("real");

                    b.Property<float>("PV_input_current_for_battery")
                        .HasColumnType("real");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("InverterData", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
