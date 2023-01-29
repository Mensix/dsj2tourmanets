using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dsj2TournamentsServer.Migrations
{
    /// <inheritdoc />
    public partial class MarkCreatedByAsRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tournaments_users_created_by_id",
                table: "tournaments");

            migrationBuilder.AlterColumn<int>(
                name: "created_by_id",
                table: "tournaments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_tournaments_users_created_by_id",
                table: "tournaments",
                column: "created_by_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tournaments_users_created_by_id",
                table: "tournaments");

            migrationBuilder.AlterColumn<int>(
                name: "created_by_id",
                table: "tournaments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "fk_tournaments_users_created_by_id",
                table: "tournaments",
                column: "created_by_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
