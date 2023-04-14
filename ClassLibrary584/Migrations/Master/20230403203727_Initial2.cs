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
                name: "NovelLibary",
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
                    table.PrimaryKey("PK_NovelLibary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasternNovelLibary",
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
                    table.PrimaryKey("PK_EasternNovelLibary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EasternNovelLibary_NovelLibary_NovelId",
                        column: x => x.NovelId,
                        principalTable: "NovelLibary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NovelLibary_ISO2",
                table: "NovelLibary",
                column: "ISO2");

            migrationBuilder.CreateIndex(
                name: "IX_NovelLibary_ISO3",
                table: "NovelLibary",
                column: "ISO3");

            migrationBuilder.CreateIndex(
                name: "IX_NovelLibary_Name",
                table: "NovelLibary",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EasternNovelLibary_Lat",
                table: "EasternNovelLibary",
                column: "Lat");

            migrationBuilder.CreateIndex(
                name: "IX_EasternNovelLibary_Lon",
                table: "EasternNovelLibary",
                column: "Lon");

            migrationBuilder.CreateIndex(
                name: "IX_EasternNovelLibary_Name",
                table: "EasternNovelLibary",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EasternNovelLibary_NovelId",
                table: "EasternNovelLibary",
                column: "NovelId");
        }

        /// <inheritdoc />
       
    }
}
