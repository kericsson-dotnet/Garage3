using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParkingEvents_VehicleId",
                table: "ParkingEvents");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingEvents_VehicleId",
                table: "ParkingEvents",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParkingEvents_VehicleId",
                table: "ParkingEvents");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingEvents_VehicleId",
                table: "ParkingEvents",
                column: "VehicleId",
                unique: true);
        }
    }
}
