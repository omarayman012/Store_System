using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Sys.Migrations
{
    /// <inheritdoc />
    public partial class InitialOutMaterialsfinall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OutMaterialsId",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OutMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialCode = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OutputType = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                        name: "FK_OutMaterials_Materials_MaterialCode",
                        column: x => x.MaterialCode,
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
                name: "IX_Materials_OutMaterialsId",
                table: "Materials",
                column: "OutMaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMaterials_DepartmentId",
                table: "OutMaterials",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMaterials_MaterialCode",
                table: "OutMaterials",
                column: "MaterialCode");

            migrationBuilder.CreateIndex(
                name: "IX_OutMaterials_PersonId",
                table: "OutMaterials",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_OutMaterials_OutMaterialsId",
                table: "Materials",
                column: "OutMaterialsId",
                principalTable: "OutMaterials",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_OutMaterials_OutMaterialsId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "OutMaterials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_OutMaterialsId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "OutMaterialsId",
                table: "Materials");
        }
    }
}
