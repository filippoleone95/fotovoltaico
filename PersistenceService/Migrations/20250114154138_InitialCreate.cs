using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersistenceService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InverterData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Grid_voltage = table.Column<float>(type: "real", nullable: false),
                    Grid_frequency = table.Column<float>(type: "real", nullable: false),
                    AC_output_voltage = table.Column<float>(type: "real", nullable: false),
                    AC_output_frequency = table.Column<float>(type: "real", nullable: false),
                    AC_output_apparent_power = table.Column<float>(type: "real", nullable: false),
                    AC_output_active_power = table.Column<float>(type: "real", nullable: false),
                    Output_Load_Percent = table.Column<float>(type: "real", nullable: false),
                    Bus_voltage = table.Column<float>(type: "real", nullable: false),
                    Battery_voltage = table.Column<float>(type: "real", nullable: false),
                    Battery_charging_current = table.Column<float>(type: "real", nullable: false),
                    Battery_capacity = table.Column<float>(type: "real", nullable: false),
                    Inverter_heatsink_temperature = table.Column<float>(type: "real", nullable: false),
                    PV_input_current_for_battery = table.Column<float>(type: "real", nullable: false),
                    PV_Input_Voltage = table.Column<float>(type: "real", nullable: false),
                    Battery_voltage_from_SCC = table.Column<float>(type: "real", nullable: false),
                    Battery_discharge_current = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InverterData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InverterData");
        }
    }
}
