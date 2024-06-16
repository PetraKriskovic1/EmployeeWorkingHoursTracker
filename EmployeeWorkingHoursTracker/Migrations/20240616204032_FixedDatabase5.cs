using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeWorkingHoursTracker.Migrations
{
    /// <inheritdoc />
    public partial class FixedDatabase5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTimeAsDateTime",
                table: "timeTrackings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTimeAsDateTime",
                table: "timeTrackings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTimeAsDateTime",
                table: "timeTrackings");

            migrationBuilder.DropColumn(
                name: "StartTimeAsDateTime",
                table: "timeTrackings");
        }
    }
}
