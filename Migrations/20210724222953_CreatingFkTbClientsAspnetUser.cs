using Microsoft.EntityFrameworkCore.Migrations;

namespace SantafeApi.Migrations
{
    public partial class CreatingFkTbClientsAspnetUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tbCliente_CodCliente",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tbCliente_CodCliente",
                table: "AspNetUsers",
                column: "CodCliente",
                principalTable: "tbCliente",
                principalColumn: "CodCliente",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tbCliente_CodCliente",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tbCliente_CodCliente",
                table: "AspNetUsers",
                column: "CodCliente",
                principalTable: "tbCliente",
                principalColumn: "CodCliente",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
