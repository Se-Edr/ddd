using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _1st : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DaysTable",
                columns: table => new
                {
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    ShiftString = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysTable", x => x.DayId);
                });

            migrationBuilder.CreateTable(
                name: "SettingsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BaseWindowInMinutes = table.Column<int>(type: "int", nullable: false),
                    BasePricePerWindow = table.Column<int>(type: "int", nullable: false),
                    EveningFinishTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EveningStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    MorningFinishTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    MorningStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    WholeDayFinishTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    WholeDayStartTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetingsTable",
                columns: table => new
                {
                    MeetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForCarSPZ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingWorkingDayDayDayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveMeeting = table.Column<bool>(type: "bit", nullable: false),
                    FinishDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ConfirmedByUser = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedByAdmin = table.Column<bool>(type: "bit", nullable: false),
                    CalculatedPrice = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingsTable", x => x.MeetId);
                    table.ForeignKey(
                        name: "FK_MeetingsTable_DaysTable_StartingWorkingDayDayDayId",
                        column: x => x.StartingWorkingDayDayDayId,
                        principalTable: "DaysTable",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProceduresTable",
                columns: table => new
                {
                    ProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcedureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakeWindows = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    MeetingMeetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProceduresTable", x => x.ProcedureId);
                    table.ForeignKey(
                        name: "FK_ProceduresTable_MeetingsTable_MeetingMeetId",
                        column: x => x.MeetingMeetId,
                        principalTable: "MeetingsTable",
                        principalColumn: "MeetId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingsTable_StartingWorkingDayDayDayId",
                table: "MeetingsTable",
                column: "StartingWorkingDayDayDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProceduresTable_MeetingMeetId",
                table: "ProceduresTable",
                column: "MeetingMeetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProceduresTable");

            migrationBuilder.DropTable(
                name: "SettingsTable");

            migrationBuilder.DropTable(
                name: "MeetingsTable");

            migrationBuilder.DropTable(
                name: "DaysTable");
        }
    }
}
