using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eclipse.Migrations
{
    /// <inheritdoc />
    public partial class ModifyComentaryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarys_Users_UserID",
                table: "Comentarys");

            migrationBuilder.DropIndex(
                name: "IX_Comentarys_UserID",
                table: "Comentarys");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Comentarys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Comentarys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comentarys_UserID",
                table: "Comentarys",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarys_Users_UserID",
                table: "Comentarys",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
