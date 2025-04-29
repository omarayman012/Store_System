using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Sys.Migrations
{
    /// <inheritdoc />
    public partial class ArchiveDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNum = table.Column<int>(type: "int", nullable: false),
                    Documentdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOfOperationId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Archive_TypeOfOperation_TypeOfOperationId",
                        column: x => x.TypeOfOperationId,
                        principalTable: "TypeOfOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Archive_TypeOfOperationId",
                table: "Archive",
                column: "TypeOfOperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archive");
        }
    }
}
