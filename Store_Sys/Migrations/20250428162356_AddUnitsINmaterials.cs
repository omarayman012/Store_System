using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Sys.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitsINmaterials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitsId",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_UnitsId",
                table: "Materials",
                column: "UnitsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Units_UnitsId",
                table: "Materials",
                column: "UnitsId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Units_UnitsId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_UnitsId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "UnitsId",
                table: "Materials");
        }
    }
}
