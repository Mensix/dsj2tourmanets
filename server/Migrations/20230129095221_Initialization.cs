using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dsj2TournamentsServer.Migrations
{
    /// <inheritdoc />
    public partial class Initialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hills",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hills", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tournament_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    liveboard = table.Column<bool>(name: "live_board", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tournament_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<decimal>(name: "user_id", type: "numeric(20,0)", nullable: false),
                    username = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tournaments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdbyid = table.Column<int>(name: "created_by_id", type: "integer", nullable: true),
                    settingsid = table.Column<int>(name: "settings_id", type: "integer", nullable: true),
                    hillid = table.Column<int>(name: "hill_id", type: "integer", nullable: false),
                    startdate = table.Column<DateTime>(name: "start_date", type: "timestamp with time zone", nullable: true),
                    enddate = table.Column<DateTime>(name: "end_date", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tournaments", x => x.id);
                    table.ForeignKey(
                        name: "fk_tournaments_hills_hill_id",
                        column: x => x.hillid,
                        principalTable: "hills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tournaments_tournament_settings_settings_id",
                        column: x => x.settingsid,
                        principalTable: "tournament_settings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tournaments_users_created_by_id",
                        column: x => x.createdbyid,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "jumps",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(name: "user_id", type: "integer", nullable: true),
                    replaycode = table.Column<string>(name: "replay_code", type: "text", nullable: false),
                    hillid = table.Column<int>(name: "hill_id", type: "integer", nullable: false),
                    player = table.Column<string>(type: "text", nullable: false),
                    length = table.Column<decimal>(type: "numeric", nullable: false),
                    crash = table.Column<bool>(type: "boolean", nullable: false),
                    points = table.Column<decimal>(type: "numeric", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    tournamentcode = table.Column<string>(name: "tournament_code", type: "text", nullable: true),
                    tournamentid = table.Column<int>(name: "tournament_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_jumps", x => x.id);
                    table.ForeignKey(
                        name: "fk_jumps_hills_hill_id",
                        column: x => x.hillid,
                        principalTable: "hills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_jumps_tournaments_tournament_id",
                        column: x => x.tournamentid,
                        principalTable: "tournaments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_jumps_users_user_id",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_jumps_hill_id",
                table: "jumps",
                column: "hill_id");

            migrationBuilder.CreateIndex(
                name: "ix_jumps_tournament_id",
                table: "jumps",
                column: "tournament_id");

            migrationBuilder.CreateIndex(
                name: "ix_jumps_user_id",
                table: "jumps",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_tournaments_created_by_id",
                table: "tournaments",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_tournaments_hill_id",
                table: "tournaments",
                column: "hill_id");

            migrationBuilder.CreateIndex(
                name: "ix_tournaments_settings_id",
                table: "tournaments",
                column: "settings_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jumps");

            migrationBuilder.DropTable(
                name: "tournaments");

            migrationBuilder.DropTable(
                name: "hills");

            migrationBuilder.DropTable(
                name: "tournament_settings");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
