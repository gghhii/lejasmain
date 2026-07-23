using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrAshrafMellouli.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsForDynamicContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProtocolUsed",
                table: "Results",
                newName: "ProtocolName");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Articles",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Excerpt",
                table: "Articles",
                newName: "CoverImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Results",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Articles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Testimonials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Procedure = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    IsFeatured = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonials", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Testimonials");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "ProtocolName",
                table: "Results",
                newName: "ProtocolUsed");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Articles",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "CoverImageUrl",
                table: "Articles",
                newName: "Excerpt");
        }
    }
}
