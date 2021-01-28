using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversitySystem.Data.Migrations
{
    public partial class AddSecondSemester : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Semesters",
                columns: new[] { "Id", "CreatedOn", "EndDate", "ModifiedOn", "SemesterName", "StartDate", "StudentId" },
                values: new object[] { 2, new DateTime(2021, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Semester 2", new DateTime(2020, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
