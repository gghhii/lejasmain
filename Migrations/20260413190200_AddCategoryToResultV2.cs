using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrAshrafMellouli.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToResultV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Results",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Results");
        }
    }
}
