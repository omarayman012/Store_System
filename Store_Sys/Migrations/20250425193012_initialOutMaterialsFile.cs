using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Sys.Migrations
{
    /// <inheritdoc />
    public partial class initialOutMaterialsFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNum = table.Column<int>(type: "int", nullable: false),
                    Documentdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNum = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OutputTypeId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    ApprovalNum = table.Column<int>(type: "int", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonPrepared = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutFiles_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OutFiles_OutputTypes_OutputTypeId",
                        column: x => x.OutputTypeId,
                        principalTable: "OutputTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutFiles_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OutFiles_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutMaterialsFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutFileId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    UnitsId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    YearDateId = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutMaterialsFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutMaterialsFile_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutMaterialsFile_OutFiles_OutFileId",
                        column: x => x.OutFileId,
                        principalTable: "OutFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutMaterialsFile_Units_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutMaterialsFile_YearsDates_YearDateId",
                        column: x => x.YearDateId,
                        principalTable: "YearsDates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutFiles_DepartmentId",
                table: "OutFiles",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OutFiles_OutputTypeId",
                table: "OutFiles",
                column: "OutputTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OutFiles_PersonId",
                table: "OutFiles",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_OutFiles_SourceId",
                table: "OutFiles",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMaterialsFile_MaterialId",
                table: "OutMaterialsFile",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMaterialsFile_OutFileId",
                table: "OutMaterialsFile",
                column: "OutFileId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMaterialsFile_UnitsId",
                table: "OutMaterialsFile",
                column: "UnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_OutMaterialsFile_YearDateId",
                table: "OutMaterialsFile",
                column: "YearDateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutMaterialsFile");

            migrationBuilder.DropTable(
                name: "OutFiles");
        }
    }
}
