using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrAshrafMellouli.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCoverVideoUrlFromArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverVideoUrl",
                table: "Articles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverVideoUrl",
                table: "Articles",
                type: "TEXT",
                nullable: true);
        }
    }
}
