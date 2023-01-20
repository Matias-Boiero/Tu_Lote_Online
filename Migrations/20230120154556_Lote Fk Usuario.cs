using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuLote.Migrations
{
    public partial class LoteFkUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Usuario_Id",
                table: "Lotes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_Usuario_Id",
                table: "Lotes",
                column: "Usuario_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lotes_AspNetUsers_Usuario_Id",
                table: "Lotes",
                column: "Usuario_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lotes_AspNetUsers_Usuario_Id",
                table: "Lotes");

            migrationBuilder.DropIndex(
                name: "IX_Lotes_Usuario_Id",
                table: "Lotes");

            migrationBuilder.DropColumn(
                name: "Usuario_Id",
                table: "Lotes");
        }
    }
}
