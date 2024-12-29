using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eclipse.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCommentary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Comentarys",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Comentarys",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Comentarys");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Comentarys");
        }
    }
}
