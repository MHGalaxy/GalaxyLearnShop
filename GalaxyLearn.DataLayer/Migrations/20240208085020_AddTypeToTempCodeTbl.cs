using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyLearn.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeToTempCodeTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "TempCodes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "TempCodes");
        }
    }
}
