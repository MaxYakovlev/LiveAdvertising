using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveAdvertising.Migrations
{
    public partial class add_title_for_stream : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Streams",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Streams");
        }
    }
}
