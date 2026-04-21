using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyJokesWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Jokes_JokeId1",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_JokeId1",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "JokeId1",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "JokeId",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_JokeId",
                table: "Comments",
                column: "JokeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Jokes_JokeId",
                table: "Comments",
                column: "JokeId",
                principalTable: "Jokes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Jokes_JokeId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_JokeId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "JokeId",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "JokeId1",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_JokeId1",
                table: "Comment",
                column: "JokeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Jokes_JokeId1",
                table: "Comment",
                column: "JokeId1",
                principalTable: "Jokes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
