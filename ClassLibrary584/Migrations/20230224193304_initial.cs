using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibrary584.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NovelLibary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISO2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISO3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "IX_Cities_NovelId",
                table: "EasternNovelLibary",
                column: "NovelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasternNovel");

            migrationBuilder.DropTable(
                name: "Novel");
        }
    }
}
