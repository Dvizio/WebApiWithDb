using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiWithDb.Migrations
{
    /// <inheritdoc />
    public partial class MoveScoreToGameScoreBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "ScoreBoard",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreBoard",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
