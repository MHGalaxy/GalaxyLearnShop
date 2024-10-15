using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyLearn.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_AddLinkToMenuGroupTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "MenuGroups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "MenuGroups");
        }
    }
}
