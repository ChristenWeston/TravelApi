using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelApi.Migrations
{
    public partial class Context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "ReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewId");
        }
    }
}
