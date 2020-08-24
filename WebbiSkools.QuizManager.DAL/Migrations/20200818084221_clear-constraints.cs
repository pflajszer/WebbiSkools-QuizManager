using Microsoft.EntityFrameworkCore.Migrations;

namespace WebbiSkools.QuizManager.Data.Migrations
{
    public partial class clearconstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_EQuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizes_EQuizId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_EQuizId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_EQuestionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "EQuizId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "EQuestionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Answers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EQuizId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EQuestionId",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_EQuizId",
                table: "Questions",
                column: "EQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_EQuestionId",
                table: "Answers",
                column: "EQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_EQuestionId",
                table: "Answers",
                column: "EQuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizes_EQuizId",
                table: "Questions",
                column: "EQuizId",
                principalTable: "Quizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
