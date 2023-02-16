using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesProject.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Notes_NoteId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_NoteId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteId",
                table: "Notes",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Notes_NoteId",
                table: "Notes",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id");
        }
    }
}
