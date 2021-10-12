using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteShared.Infrastructure.Data.Migrations
{
    public partial class UnnecessaryHistoryAndDesign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NoteDesigns_DesignID",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_HistoryID",
                table: "Notes");

            migrationBuilder.AlterColumn<int>(
                name: "HistoryID",
                table: "Notes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DesignID",
                table: "Notes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_HistoryID",
                table: "Notes",
                column: "HistoryID",
                unique: true,
                filter: "[HistoryID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NoteDesigns_DesignID",
                table: "Notes",
                column: "DesignID",
                principalTable: "NoteDesigns",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NoteDesigns_DesignID",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_HistoryID",
                table: "Notes");

            migrationBuilder.AlterColumn<int>(
                name: "HistoryID",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DesignID",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_HistoryID",
                table: "Notes",
                column: "HistoryID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NoteDesigns_DesignID",
                table: "Notes",
                column: "DesignID",
                principalTable: "NoteDesigns",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
