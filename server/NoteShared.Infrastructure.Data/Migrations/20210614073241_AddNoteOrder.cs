using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteShared.Infrastructure.Data.Migrations
{
    public partial class AddNoteOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Notes");
        }
    }
}
