using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrAshrafMellouli.Migrations
{
    /// <inheritdoc />
    public partial class AddCoverVideoUrlToArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverVideoUrl",
                table: "Articles",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverVideoUrl",
                table: "Articles");
        }
    }
}
