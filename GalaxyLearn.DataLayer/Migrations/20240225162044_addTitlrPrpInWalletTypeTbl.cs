using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyLearn.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addTitlrPrpInWalletTypeTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WalletTypeTitle",
                table: "WalletTypes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WalletTypeTitle",
                table: "WalletTypes");
        }
    }
}
