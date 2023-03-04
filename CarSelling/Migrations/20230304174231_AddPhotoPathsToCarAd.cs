using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSelling.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoPathsToCarAd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilePath",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarAdId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilePath", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilePath_CarAds_CarAdId",
                        column: x => x.CarAdId,
                        principalTable: "CarAds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilePath_CarAdId",
                table: "FilePath",
                column: "CarAdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilePath");
        }
    }
}
