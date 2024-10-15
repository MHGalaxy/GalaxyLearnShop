using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyLearn.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_AddOrderToMenuGroupTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "MenuGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "MenuGroups");
        }
    }
}
