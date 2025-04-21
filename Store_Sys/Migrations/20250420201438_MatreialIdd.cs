using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Sys.Migrations
{
    /// <inheritdoc />
    public partial class MatreialIdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutMaterials_Materials_MaterialCode",
                table: "OutMaterials");

            migrationBuilder.RenameColumn(
                name: "MaterialCode",
                table: "OutMaterials",
                newName: "MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_OutMaterials_MaterialCode",
                table: "OutMaterials",
                newName: "IX_OutMaterials_MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_OutMaterials_Materials_MaterialId",
                table: "OutMaterials",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutMaterials_Materials_MaterialId",
                table: "OutMaterials");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "OutMaterials",
                newName: "MaterialCode");

            migrationBuilder.RenameIndex(
                name: "IX_OutMaterials_MaterialId",
                table: "OutMaterials",
                newName: "IX_OutMaterials_MaterialCode");

            migrationBuilder.AddForeignKey(
                name: "FK_OutMaterials_Materials_MaterialCode",
                table: "OutMaterials",
                column: "MaterialCode",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
