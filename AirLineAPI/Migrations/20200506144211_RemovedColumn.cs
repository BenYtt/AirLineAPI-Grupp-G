using Microsoft.EntityFrameworkCore.Migrations;

namespace AirLineAPI.Migrations
{
    public partial class RemovedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_TimeTables_TimeTableID",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_TimeTableID",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "TimeTableID",
                table: "Passengers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TimeTableID",
                table: "Passengers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_TimeTableID",
                table: "Passengers",
                column: "TimeTableID");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_TimeTables_TimeTableID",
                table: "Passengers",
                column: "TimeTableID",
                principalTable: "TimeTables",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
