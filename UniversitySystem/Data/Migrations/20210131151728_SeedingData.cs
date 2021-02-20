using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversitySystem.Data.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Disciplines",
                columns: new[] { "Id", "CreatedOn", "DisciplineName", "ModifiedOn", "ProfessorName", "SemesterId" },
                values: new object[] { 1, new DateTime(2021, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Math", null, "Newton", 1 });

            migrationBuilder.InsertData(
                table: "Disciplines",
                columns: new[] { "Id", "CreatedOn", "DisciplineName", "ModifiedOn", "ProfessorName", "SemesterId" },
                values: new object[] { 2, new DateTime(2021, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Programming", null, "Mustain", 1 });

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "Id", "DisciplineId", "ScoreNumber" },
                values: new object[] { 1, 1, 5 });

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "Id", "DisciplineId", "ScoreNumber" },
                values: new object[] { 2, 2, 6 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Disciplines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Disciplines",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
