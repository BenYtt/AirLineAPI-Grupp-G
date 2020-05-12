using Microsoft.EntityFrameworkCore.Migrations;

namespace AirLineAPI.Migrations
{
    public partial class UpdatedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalNumber",
                table: "Passengers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Passengers",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdentificationNumber",
                table: "Passengers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_IdentificationNumber",
                table: "Passengers",
                column: "IdentificationNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Passengers_IdentificationNumber",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                table: "Passengers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "PersonalNumber",
                table: "Passengers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
