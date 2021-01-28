using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversitySystem.Data.Migrations
{
    public partial class ScoreModelFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Scores_ScoreInfoId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_ScoreInfoId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ScoreInfoId",
                table: "Scores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScoreInfoId",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ScoreInfoId",
                table: "Scores",
                column: "ScoreInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Scores_ScoreInfoId",
                table: "Scores",
                column: "ScoreInfoId",
                principalTable: "Scores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
