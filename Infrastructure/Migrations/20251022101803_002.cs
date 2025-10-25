using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EveningFinishTime",
                table: "SettingsTable");

            migrationBuilder.DropColumn(
                name: "EveningStartTime",
                table: "SettingsTable");

            migrationBuilder.AddColumn<bool>(
                name: "fixedPrice",
                table: "ProceduresTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fixedPrice",
                table: "ProceduresTable");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EveningFinishTime",
                table: "SettingsTable",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EveningStartTime",
                table: "SettingsTable",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
