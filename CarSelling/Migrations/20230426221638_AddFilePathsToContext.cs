using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSelling.Migrations
{
    /// <inheritdoc />
    public partial class AddFilePathsToContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilePath_CarAds_CarAdId",
                table: "FilePath");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilePath",
                table: "FilePath");

            migrationBuilder.RenameTable(
                name: "FilePath",
                newName: "FilePaths");

            migrationBuilder.RenameIndex(
                name: "IX_FilePath_CarAdId",
                table: "FilePaths",
                newName: "IX_FilePaths_CarAdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilePaths",
                table: "FilePaths",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FilePaths_CarAds_CarAdId",
                table: "FilePaths",
                column: "CarAdId",
                principalTable: "CarAds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilePaths_CarAds_CarAdId",
                table: "FilePaths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilePaths",
                table: "FilePaths");

            migrationBuilder.RenameTable(
                name: "FilePaths",
                newName: "FilePath");

            migrationBuilder.RenameIndex(
                name: "IX_FilePaths_CarAdId",
                table: "FilePath",
                newName: "IX_FilePath_CarAdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilePath",
                table: "FilePath",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FilePath_CarAds_CarAdId",
                table: "FilePath",
                column: "CarAdId",
                principalTable: "CarAds",
                principalColumn: "Id");
        }
    }
}
