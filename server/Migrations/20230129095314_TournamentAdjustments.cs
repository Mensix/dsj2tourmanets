using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dsj2TournamentsServer.Migrations
{
    /// <inheritdoc />
    public partial class TournamentAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "tournaments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "tournaments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "tournaments");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "tournaments");
        }
    }
}
