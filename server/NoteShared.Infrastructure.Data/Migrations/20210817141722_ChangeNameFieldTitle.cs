using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteShared.Infrastructure.Data.Migrations
{
    public partial class ChangeNameFieldTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "NoteText",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "NoteText",
                newName: "Tittle");
        }
    }
}
