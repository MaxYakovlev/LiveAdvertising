using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveAdvertising.Migrations
{
    public partial class add_isactive_for_stream : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Streams",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Streams");
        }
    }
}
