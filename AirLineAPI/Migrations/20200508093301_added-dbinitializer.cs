using Microsoft.EntityFrameworkCore.Migrations;

namespace AirLineAPI.Migrations
{
    public partial class addeddbinitializer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PersonalNumber",
                table: "Passengers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PersonalNumber",
                table: "Passengers",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
