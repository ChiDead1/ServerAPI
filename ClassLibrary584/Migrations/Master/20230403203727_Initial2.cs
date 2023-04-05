using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibrary584.Migrations.Master
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ISO2 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ISO3 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasternNovel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lat = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    Lon = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    NovelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasternNovel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EasternNovel_Countries_NovelId",
                        column: x => x.NovelId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ISO2",
                table: "Countries",
                column: "ISO2");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ISO3",
                table: "Countries",
                column: "ISO3");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EasternNovel_Lat",
                table: "EasternNovel",
                column: "Lat");

            migrationBuilder.CreateIndex(
                name: "IX_EasternNovel_Lon",
                table: "EasternNovel",
                column: "Lon");

            migrationBuilder.CreateIndex(
                name: "IX_EasternNovel_Name",
                table: "EasternNovel",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EasternNovel_NovelId",
                table: "EasternNovel",
                column: "NovelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasternNovel");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
