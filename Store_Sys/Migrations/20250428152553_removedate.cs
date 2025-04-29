using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Sys.Migrations
{
    /// <inheritdoc />
    public partial class removedate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InMaterialsFiles_YearsDates_YearDateId",
                table: "InMaterialsFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_OutMaterialsFile_YearsDates_YearDateId",
                table: "OutMaterialsFile");

            migrationBuilder.DropIndex(
                name: "IX_OutMaterialsFile_YearDateId",
                table: "OutMaterialsFile");

            migrationBuilder.DropIndex(
                name: "IX_InMaterialsFiles_YearDateId",
                table: "InMaterialsFiles");

            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "OutMaterialsFile");

            migrationBuilder.DropColumn(
                name: "YearDateId",
                table: "OutMaterialsFile");

            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "InMaterialsFiles");

            migrationBuilder.DropColumn(
                name: "YearDateId",
                table: "InMaterialsFiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "OutMaterialsFile",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "YearDateId",
                table: "OutMaterialsFile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "InMaterialsFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "YearDateId",
                table: "InMaterialsFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OutMaterialsFile_YearDateId",
                table: "OutMaterialsFile",
                column: "YearDateId");

            migrationBuilder.CreateIndex(
                name: "IX_InMaterialsFiles_YearDateId",
                table: "InMaterialsFiles",
                column: "YearDateId");

            migrationBuilder.AddForeignKey(
                name: "FK_InMaterialsFiles_YearsDates_YearDateId",
                table: "InMaterialsFiles",
                column: "YearDateId",
                principalTable: "YearsDates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutMaterialsFile_YearsDates_YearDateId",
                table: "OutMaterialsFile",
                column: "YearDateId",
                principalTable: "YearsDates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
