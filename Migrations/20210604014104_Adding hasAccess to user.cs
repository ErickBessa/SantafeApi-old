using Microsoft.EntityFrameworkCore.Migrations;

namespace SantafeApi.Migrations
{
    public partial class AddinghasAccesstouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "hasAccess",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasAccess",
                table: "AspNetUsers");
        }
    }
}
