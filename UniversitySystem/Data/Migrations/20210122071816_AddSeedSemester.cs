using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversitySystem.Data.Migrations
{
    public partial class AddSeedSemester : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Semesters",
                columns: new[] { "Id", "CreatedOn", "EndDate", "ModifiedOn", "SemesterName", "StartDate", "StudentId" },
                values: new object[] { 1, new DateTime(2021, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Semester 1", new DateTime(2019, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
