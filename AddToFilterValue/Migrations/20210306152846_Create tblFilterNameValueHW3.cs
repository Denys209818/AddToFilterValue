using Microsoft.EntityFrameworkCore.Migrations;

namespace AddToFilterValue.Migrations
{
    public partial class CreatetblFilterNameValueHW3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFilterNameValueHW3",
                columns: table => new
                {
                    FilterNameId = table.Column<int>(type: "integer", nullable: false),
                    FilterValueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFilterNameValueHW3", x => new { x.FilterNameId, x.FilterValueId });
                    table.ForeignKey(
                        name: "FK_tblFilterNameValueHW3_tblFilterNameHW3_FilterNameId",
                        column: x => x.FilterNameId,
                        principalTable: "tblFilterNameHW3",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFilterNameValueHW3_tblFilterValueHW3_FilterValueId",
                        column: x => x.FilterValueId,
                        principalTable: "tblFilterValueHW3",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFilterNameValueHW3_FilterValueId",
                table: "tblFilterNameValueHW3",
                column: "FilterValueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFilterNameValueHW3");
        }
    }
}
