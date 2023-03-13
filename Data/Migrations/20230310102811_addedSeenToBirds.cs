using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdsSweden300.api.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedSeenToBirds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Birds",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Birds");
        }
    }
}
