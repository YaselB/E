using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Eclipse.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropColumn(
                name: "selectedDates",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "Dates",
                columns: table => new
                {
                    IDDates = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<string>(type: "text", nullable: false),
                    IDTickets = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dates", x => x.IDDates);
                    table.ForeignKey(
                        name: "FK_Dates_Tickets_IDTickets",
                        column: x => x.IDTickets,
                        principalTable: "Tickets",
                        principalColumn: "IDTicket",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    IDPrices = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    currency = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    IDTickets = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.IDPrices);
                    table.ForeignKey(
                        name: "FK_Prices_Tickets_IDTickets",
                        column: x => x.IDTickets,
                        principalTable: "Tickets",
                        principalColumn: "IDTicket",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dates_IDTickets",
                table: "Dates",
                column: "IDTickets");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_IDTickets",
                table: "Prices",
                column: "IDTickets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dates");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.AddColumn<string[]>(
                name: "selectedDates",
                table: "Tickets",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    IDPrice = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TicketID = table.Column<int>(type: "integer", nullable: false),
                    Money = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.IDPrice);
                    table.ForeignKey(
                        name: "FK_Price_Tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "Tickets",
                        principalColumn: "IDTicket",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Price_TicketID",
                table: "Price",
                column: "TicketID");
        }
    }
}
