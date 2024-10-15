using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalaxyLearn.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_CourseGroupToMenuGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseGroups");

            migrationBuilder.CreateTable(
                name: "MenuGroups",
                columns: table => new
                {
                    MenuGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuGroupName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MenuGroupTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuGroups", x => x.MenuGroupId);
                    table.ForeignKey(
                        name: "FK_MenuGroups_MenuGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MenuGroups",
                        principalColumn: "MenuGroupId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuGroups_ParentId",
                table: "MenuGroups",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuGroups");

            migrationBuilder.CreateTable(
                name: "CourseGroups",
                columns: table => new
                {
                    CourseGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseGroupName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CourseGroupTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroups", x => x.CourseGroupId);
                    table.ForeignKey(
                        name: "FK_CourseGroups_CourseGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CourseGroups",
                        principalColumn: "CourseGroupId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroups_ParentId",
                table: "CourseGroups",
                column: "ParentId");
        }
    }
}
