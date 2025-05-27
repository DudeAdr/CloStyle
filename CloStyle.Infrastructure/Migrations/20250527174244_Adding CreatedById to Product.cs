using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloStyle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingCreatedByIdtoProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Products");
        }
    }
}
