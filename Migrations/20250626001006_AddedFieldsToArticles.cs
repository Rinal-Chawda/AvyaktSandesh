using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvyaktSandesh.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldsToArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "MediaFiles");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Articles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "MediaFiles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
