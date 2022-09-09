using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace URSBackend.Migrations
{
    public partial class Schedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Day1",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Day2",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time1",
                table: "Classrooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time2",
                table: "Classrooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day1",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "Day2",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "Time1",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "Time2",
                table: "Classrooms");
        }
    }
}
