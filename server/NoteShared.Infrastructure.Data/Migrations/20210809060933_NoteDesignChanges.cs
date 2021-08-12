using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteShared.Infrastructure.Data.Migrations
{
    public partial class NoteDesignChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NoteDesigns_DesignID",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_DesignID",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "NoteID",
                table: "NoteDesigns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NoteDesigns_NoteID",
                table: "NoteDesigns",
                column: "NoteID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteDesigns_Notes_NoteID",
                table: "NoteDesigns",
                column: "NoteID",
                principalTable: "Notes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteDesigns_Notes_NoteID",
                table: "NoteDesigns");

            migrationBuilder.DropIndex(
                name: "IX_NoteDesigns_NoteID",
                table: "NoteDesigns");

            migrationBuilder.DropColumn(
                name: "NoteID",
                table: "NoteDesigns");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_DesignID",
                table: "Notes",
                column: "DesignID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NoteDesigns_DesignID",
                table: "Notes",
                column: "DesignID",
                principalTable: "NoteDesigns",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
