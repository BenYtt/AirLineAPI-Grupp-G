using Microsoft.EntityFrameworkCore.Migrations;

namespace AirLineAPI.Migrations
{
    public partial class UserAndPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassengerTimeTables_Passengers_PassengerId",
                table: "PassengerTimeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PassengerTimeTables_TimeTables_TimeTableId",
                table: "PassengerTimeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Flights_FlightID",
                table: "TimeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Routes_RouteID",
                table: "TimeTables");

            migrationBuilder.RenameColumn(
                name: "RouteID",
                table: "TimeTables",
                newName: "RouteId");

            migrationBuilder.RenameColumn(
                name: "FlightID",
                table: "TimeTables",
                newName: "FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTables_RouteID",
                table: "TimeTables",
                newName: "IX_TimeTables_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTables_FlightID",
                table: "TimeTables",
                newName: "IX_TimeTables_FlightId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Routes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TimeTableId",
                table: "PassengerTimeTables",
                newName: "TimeTableID");

            migrationBuilder.RenameColumn(
                name: "PassengerId",
                table: "PassengerTimeTables",
                newName: "PassengerID");

            migrationBuilder.RenameIndex(
                name: "IX_PassengerTimeTables_TimeTableId",
                table: "PassengerTimeTables",
                newName: "IX_PassengerTimeTables_TimeTableID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Flights",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ApiKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerTimeTables_Passengers_PassengerID",
                table: "PassengerTimeTables",
                column: "PassengerID",
                principalTable: "Passengers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerTimeTables_TimeTables_TimeTableID",
                table: "PassengerTimeTables",
                column: "TimeTableID",
                principalTable: "TimeTables",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Flights_FlightId",
                table: "TimeTables",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Routes_RouteId",
                table: "TimeTables",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassengerTimeTables_Passengers_PassengerID",
                table: "PassengerTimeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_PassengerTimeTables_TimeTables_TimeTableID",
                table: "PassengerTimeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Flights_FlightId",
                table: "TimeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Routes_RouteId",
                table: "TimeTables");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "TimeTables",
                newName: "RouteID");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "TimeTables",
                newName: "FlightID");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTables_RouteId",
                table: "TimeTables",
                newName: "IX_TimeTables_RouteID");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTables_FlightId",
                table: "TimeTables",
                newName: "IX_TimeTables_FlightID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Routes",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "TimeTableID",
                table: "PassengerTimeTables",
                newName: "TimeTableId");

            migrationBuilder.RenameColumn(
                name: "PassengerID",
                table: "PassengerTimeTables",
                newName: "PassengerId");

            migrationBuilder.RenameIndex(
                name: "IX_PassengerTimeTables_TimeTableID",
                table: "PassengerTimeTables",
                newName: "IX_PassengerTimeTables_TimeTableId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Flights",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerTimeTables_Passengers_PassengerId",
                table: "PassengerTimeTables",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerTimeTables_TimeTables_TimeTableId",
                table: "PassengerTimeTables",
                column: "TimeTableId",
                principalTable: "TimeTables",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Flights_FlightID",
                table: "TimeTables",
                column: "FlightID",
                principalTable: "Flights",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Routes_RouteID",
                table: "TimeTables",
                column: "RouteID",
                principalTable: "Routes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
