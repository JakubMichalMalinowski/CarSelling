using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSelling.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByToCarAd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "CarAds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarAds_CreatedById",
                table: "CarAds",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CarAds_Users_CreatedById",
                table: "CarAds",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarAds_Users_CreatedById",
                table: "CarAds");

            migrationBuilder.DropIndex(
                name: "IX_CarAds_CreatedById",
                table: "CarAds");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CarAds");
        }
    }
}
