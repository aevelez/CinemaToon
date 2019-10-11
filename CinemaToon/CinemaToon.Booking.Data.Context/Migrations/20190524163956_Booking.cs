using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaToon.Booking.Data.Context.Migrations
{
    public partial class Booking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CinemaBooking",
                columns: table => new
                {
                    CinemaFuctionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: false),
                    FunctionDateTime = table.Column<DateTime>(nullable: false),
                    BasePrice = table.Column<double>(nullable: false),
                    AvailableSeats = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaBooking", x => x.CinemaFuctionId);
                });

            migrationBuilder.CreateTable(
                name: "CinemaReservations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User = table.Column<string>(nullable: true),
                    TotalPaid = table.Column<double>(nullable: false),
                    CinemaFunctionId = table.Column<int>(nullable: false),
                    TotalTickets = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    OriginalMovieTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CinemaReservations_CinemaBooking_CinemaFunctionId",
                        column: x => x.CinemaFunctionId,
                        principalTable: "CinemaBooking",
                        principalColumn: "CinemaFuctionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CinemaReservations_CinemaFunctionId",
                table: "CinemaReservations",
                column: "CinemaFunctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CinemaReservations");

            migrationBuilder.DropTable(
                name: "CinemaBooking");
        }
    }
}
