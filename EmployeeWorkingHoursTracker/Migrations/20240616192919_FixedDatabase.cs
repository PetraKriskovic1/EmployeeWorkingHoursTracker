using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeWorkingHoursTracker.Migrations
{
    /// <inheritdoc />
    public partial class FixedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracking_employees_EmployeeId",
                table: "TimeTracking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeTracking",
                table: "TimeTracking");

            migrationBuilder.RenameTable(
                name: "TimeTracking",
                newName: "timeTrackings");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTracking_EmployeeId",
                table: "timeTrackings",
                newName: "IX_timeTrackings_EmployeeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "timeTrackings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_timeTrackings",
                table: "timeTrackings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_timeTrackings_employees_EmployeeId",
                table: "timeTrackings",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timeTrackings_employees_EmployeeId",
                table: "timeTrackings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_timeTrackings",
                table: "timeTrackings");

            migrationBuilder.RenameTable(
                name: "timeTrackings",
                newName: "TimeTracking");

            migrationBuilder.RenameIndex(
                name: "IX_timeTrackings_EmployeeId",
                table: "TimeTracking",
                newName: "IX_TimeTracking_EmployeeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "TimeTracking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeTracking",
                table: "TimeTracking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracking_employees_EmployeeId",
                table: "TimeTracking",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
