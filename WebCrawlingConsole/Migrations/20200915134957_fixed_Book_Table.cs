using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCrawlingConsole.Migrations
{
    public partial class fixed_Book_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Books",
                type: "text",
                nullable: true);
        }
    }
}
