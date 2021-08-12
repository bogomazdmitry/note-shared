using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteShared.Infrastructure.Data.Migrations
{
    public partial class CreateNoteText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Tittle",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "NoteTextID",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NoteText",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tittle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteText", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteTextID",
                table: "Notes",
                column: "NoteTextID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NoteText_NoteTextID",
                table: "Notes",
                column: "NoteTextID",
                principalTable: "NoteText",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NoteText_NoteTextID",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "NoteText");

            migrationBuilder.DropIndex(
                name: "IX_Notes_NoteTextID",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "NoteTextID",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tittle",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
