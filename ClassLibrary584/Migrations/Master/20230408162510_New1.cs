using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibrary584.Migrations.Master
{
    /// <inheritdoc />
    public partial class New1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EasternNovelLibary_NovelLibary_NovelId",
                table: "EasternNovelLibary");

            migrationBuilder.RenameColumn(
                name: "NovelId",
                table: "EasternNovelLibary",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_EasternNovelLibary_NovelId",
                table: "EasternNovelLibary",
                newName: "IX_EasternNovelLibary_CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EasternNovelLibary_NovelLibary_CountryId",
                table: "EasternNovelLibary",
                column: "CountryId",
                principalTable: "NovelLibary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EasternNovelLibary_NovelLibary_CountryId",
                table: "EasternNovelLibary");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "EasternNovelLibary",
                newName: "NovelId");

            migrationBuilder.RenameIndex(
                name: "IX_EasternNovelLibary_CountryId",
                table: "EasternNovelLibary",
                newName: "IX_EasternNovelLibary_NovelId");

            migrationBuilder.AddForeignKey(
                name: "FK_EasternNovelLibary_NovelLibary_NovelId",
                table: "EasternNovelLibary",
                column: "NovelId",
                principalTable: "NovelLibary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
