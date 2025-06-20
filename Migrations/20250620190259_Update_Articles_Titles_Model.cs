using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvyaktSandesh.Migrations
{
    /// <inheritdoc />
    public partial class Update_Articles_Titles_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Titles_TitlesId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_TitlesId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "TitlesId",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Titles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MediaFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "MediaFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TitleId",
                table: "Articles",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Titles_TitleId",
                table: "Articles",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Titles_TitleId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_TitleId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MediaFiles");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "MediaFiles");

            migrationBuilder.AddColumn<int>(
                name: "TitlesId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TitlesId",
                table: "Articles",
                column: "TitlesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Titles_TitlesId",
                table: "Articles",
                column: "TitlesId",
                principalTable: "Titles",
                principalColumn: "Id");
        }
    }
}
