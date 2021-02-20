using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversitySystem.Data.Migrations
{
    public partial class FixScoreModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Semesters_SemesterId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_SemesterId",
                table: "Scores");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "DisiplineName",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ProfessorName",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Scores");

            migrationBuilder.AddColumn<int>(
                name: "DisciplineId",
                table: "Scores",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_DisciplineId",
                table: "Scores",
                column: "DisciplineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Disciplines_DisciplineId",
                table: "Scores",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Disciplines_DisciplineId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_DisciplineId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "DisciplineId",
                table: "Scores");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Scores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DisiplineName",
                table: "Scores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Scores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessorName",
                table: "Scores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "Id", "CreatedOn", "DisiplineName", "ModifiedOn", "ProfessorName", "ScoreNumber", "SemesterId" },
                values: new object[] { 1, new DateTime(2021, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Math", null, "Newton", 4, 1 });

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "Id", "CreatedOn", "DisiplineName", "ModifiedOn", "ProfessorName", "ScoreNumber", "SemesterId" },
                values: new object[] { 2, new DateTime(2021, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "History", null, "Ivanov", 5, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_SemesterId",
                table: "Scores",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Semesters_SemesterId",
                table: "Scores",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
