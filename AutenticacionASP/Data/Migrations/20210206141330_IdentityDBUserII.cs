using Microsoft.EntityFrameworkCore.Migrations;

namespace AutenticacionASP.Data.Migrations
{
    public partial class IdentityDBUserII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LugarDeNacimiento",
                table: "AspNetUsers",
                maxLength: 120,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LugarDeNacimiento",
                table: "AspNetUsers");
        }
    }
}
