using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Migrations
{
    public partial class photos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProductId",
                table: "Photos",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Products_ProductId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ProductId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Photos");
        }
    }
}
