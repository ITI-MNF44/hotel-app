using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_app.Migrations
{
    /// <inheritdoc />
    public partial class imageversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Hotels");
        }
    }
}
