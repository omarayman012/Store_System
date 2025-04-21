using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Sys.Migrations
{
    /// <inheritdoc />
    public partial class InitialOutForignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InMaterials_Materials_MaterialId",
                table: "InMaterials");

            migrationBuilder.DropTable(
                name: "OutMaterials");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "InMaterials",
                newName: "MaterialCode");

            migrationBuilder.RenameIndex(
                name: "IX_InMaterials_MaterialId",
                table: "InMaterials",
                newName: "IX_InMaterials_MaterialCode");

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Materials",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "InMaterialsId",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_InMaterialsId",
                table: "Materials",
                column: "InMaterialsId");

            migrationBuilder.AddForeignKey(
                name: "FK_InMaterials_Materials_MaterialCode",
                table: "InMaterials",
                column: "MaterialCode",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_InMaterials_InMaterialsId",
                table: "Materials",
                column: "InMaterialsId",
                principalTable: "InMaterials",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InMaterials_Materials_MaterialCode",
                table: "InMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_InMaterials_InMaterialsId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_InMaterialsId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "InMaterialsId",
                table: "Materials");

            migrationBuilder.RenameColumn(
                name: "MaterialCode",
                table: "InMaterials",
                newName: "MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_InMaterials_MaterialCode",
                table: "InMaterials",
                newName: "IX_InMaterials_MaterialId");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "OutMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputType = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutMaterials_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutMaterials_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutMaterials_DepartmentId",
                table: "OutMaterials",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMaterials_MaterialId",
                table: "OutMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMaterials_PersonId",
                table: "OutMaterials",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_InMaterials_Materials_MaterialId",
                table: "InMaterials",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
