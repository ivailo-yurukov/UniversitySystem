using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversitySystem.Data.Migrations
{
    public partial class AddSeedScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "Id", "CreatedOn", "DisiplineName", "ModifiedOn", "ProfessorName", "ScoreNumber", "SemesterId" },
                values: new object[] { 1, new DateTime(2021, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Math", null, "Newton", 4, 1 });

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "Id", "CreatedOn", "DisiplineName", "ModifiedOn", "ProfessorName", "ScoreNumber", "SemesterId" },
                values: new object[] { 2, new DateTime(2021, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "History", null, "Ivanov", 5, 1 });
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
        }
    }
}
