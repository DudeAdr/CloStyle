using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloStyle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingImgfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Brands");
        }
    }
}
