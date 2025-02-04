using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersistenceService.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToInverterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "InverterData",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "InverterData");
        }
    }
}
