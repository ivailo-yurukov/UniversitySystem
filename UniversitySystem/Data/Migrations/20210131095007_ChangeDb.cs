using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversitySystem.Data.Migrations
{
    public partial class ChangeDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Students_StudentId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_StudentId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Semesters");

            migrationBuilder.CreateTable(
                name: "DisciplineViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplineName = table.Column<string>(maxLength: 100, nullable: false),
                    ProfessorName = table.Column<string>(maxLength: 50, nullable: false),
                    SemesterId = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisciplineViewModel_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentSemesters",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    SemesterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSemesters", x => new { x.StudentId, x.SemesterId });
                    table.ForeignKey(
                        name: "FK_StudentSemesters_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentSemesters_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "StudentSemesters",
                columns: new[] { "StudentId", "SemesterId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "StudentSemesters",
                columns: new[] { "StudentId", "SemesterId" },
                values: new object[] { 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineViewModel_SemesterId",
                table: "DisciplineViewModel",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSemesters_SemesterId",
                table: "StudentSemesters",
                column: "SemesterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplineViewModel");

            migrationBuilder.DropTable(
                name: "StudentSemesters");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Semesters",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: 1,
                column: "StudentId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: 2,
                column: "StudentId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_StudentId",
                table: "Semesters",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Students_StudentId",
                table: "Semesters",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
