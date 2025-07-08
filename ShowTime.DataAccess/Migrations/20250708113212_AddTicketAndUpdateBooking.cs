using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowTime.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketAndUpdateBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Artists_ArtistID",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Festivals_FestivalID",
                table: "Lineups");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ArtistID",
                table: "Lineups",
                newName: "ArtistId");

            migrationBuilder.RenameColumn(
                name: "FestivalID",
                table: "Lineups",
                newName: "FestivalId");

            migrationBuilder.RenameIndex(
                name: "IX_Lineups_ArtistID",
                table: "Lineups",
                newName: "IX_Lineups_ArtistId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Bookings",
                newName: "TicketId");

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    TicketType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TicketId",
                table: "Bookings",
                column: "TicketId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Tickets_TicketId",
                table: "Bookings",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Artists_ArtistId",
                table: "Lineups",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Festivals_FestivalId",
                table: "Lineups",
                column: "FestivalId",
                principalTable: "Festivals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Tickets_TicketId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Artists_ArtistId",
                table: "Lineups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lineups_Festivals_FestivalId",
                table: "Lineups");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TicketId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Lineups",
                newName: "ArtistID");

            migrationBuilder.RenameColumn(
                name: "FestivalId",
                table: "Lineups",
                newName: "FestivalID");

            migrationBuilder.RenameIndex(
                name: "IX_Lineups_ArtistId",
                table: "Lineups",
                newName: "IX_Lineups_ArtistID");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Bookings",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Bookings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Artists_ArtistID",
                table: "Lineups",
                column: "ArtistID",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lineups_Festivals_FestivalID",
                table: "Lineups",
                column: "FestivalID",
                principalTable: "Festivals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
