using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Sys.Migrations
{
    /// <inheritdoc />
    public partial class initialInFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNum = table.Column<int>(type: "int", nullable: false),
                    Documentdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameSupplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNum = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalNum = table.Column<int>(type: "int", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InFiles_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InMaterialsFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InFileId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_InMaterialsFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InMaterialsFiles_InFiles_InFileId",
                        column: x => x.InFileId,
                        principalTable: "InFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InMaterialsFiles_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InMaterialsFiles_Units_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InMaterialsFiles_YearsDates_YearDateId",
                        column: x => x.YearDateId,
                        principalTable: "YearsDates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InFiles_SourceId",
                table: "InFiles",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_InMaterialsFiles_InFileId",
                table: "InMaterialsFiles",
                column: "InFileId");

            migrationBuilder.CreateIndex(
                name: "IX_InMaterialsFiles_MaterialId",
                table: "InMaterialsFiles",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_InMaterialsFiles_UnitsId",
                table: "InMaterialsFiles",
                column: "UnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_InMaterialsFiles_YearDateId",
                table: "InMaterialsFiles",
                column: "YearDateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InMaterialsFiles");

            migrationBuilder.DropTable(
                name: "InFiles");
        }
    }
}
