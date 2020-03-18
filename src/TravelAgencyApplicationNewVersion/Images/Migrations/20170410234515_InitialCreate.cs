using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TravelAgencyApplicationNewVersion.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "varchar(max)", nullable: false),
                    Member = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    SeatID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Airline = table.Column<string>(type: "varchar(max)", nullable: false),
                    ArrivalCity = table.Column<string>(type: "varchar(max)", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    DepartureCity = table.Column<string>(type: "varchar(max)", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    FirstClass = table.Column<bool>(nullable: false),
                    FlightID = table.Column<int>(nullable: false),
                    FlightPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.SeatID);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    RoomID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(type: "varchar(max)", nullable: false),
                    HotelID = table.Column<int>(nullable: false),
                    HotelName = table.Column<string>(type: "varchar(max)", nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.RoomID);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerID = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservation_Customer",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationDetails",
                columns: table => new
                {
                    ReservationDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArrivalTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "date", nullable: false),
                    Breakfast = table.Column<bool>(nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    FirstClass = table.Column<bool>(nullable: true),
                    Insurance = table.Column<bool>(nullable: false),
                    InvoiceExpiryDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ReservationID = table.Column<int>(nullable: false),
                    RoomID = table.Column<int>(nullable: true),
                    SeatID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDetails", x => x.ReservationDetailsID);
                    table.ForeignKey(
                        name: "FK_ReservationDetails_Flight",
                        column: x => x.ReservationID,
                        principalTable: "Reservation",
                        principalColumn: "ReservationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationDetails_Hotel",
                        column: x => x.RoomID,
                        principalTable: "Hotel",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationDetails_Flight1",
                        column: x => x.SeatID,
                        principalTable: "Flight",
                        principalColumn: "SeatID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CustomerID",
                table: "Reservation",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_ReservationID",
                table: "ReservationDetails",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_RoomID",
                table: "ReservationDetails",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_SeatID",
                table: "ReservationDetails",
                column: "SeatID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationDetails");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
