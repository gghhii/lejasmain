using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrAshrafMellouli.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    DatePublished = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Excerpt = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CaseNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    BeforeImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    AfterImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ProtocolUsed = table.Column<string>(type: "TEXT", nullable: true),
                    Sessions = table.Column<string>(type: "TEXT", nullable: true),
                    ResultType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    ProtocolDetails = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Treatments");
        }
    }
}
