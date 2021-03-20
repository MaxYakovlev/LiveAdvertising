using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveAdvertising.Migrations
{
    public partial class add_source_for_stream : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Streams",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "Streams");
        }
    }
}
