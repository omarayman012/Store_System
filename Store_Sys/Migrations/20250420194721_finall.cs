using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store_Sys.Migrations
{
    /// <inheritdoc />
    public partial class finall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutMaterials_Departments_DepartmentId",
                table: "OutMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_OutMaterials_Persons_PersonId",
                table: "OutMaterials");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "OutMaterials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "OutMaterials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OutMaterials_Departments_DepartmentId",
                table: "OutMaterials",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OutMaterials_Persons_PersonId",
                table: "OutMaterials",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutMaterials_Departments_DepartmentId",
                table: "OutMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_OutMaterials_Persons_PersonId",
                table: "OutMaterials");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "OutMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "OutMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OutMaterials_Departments_DepartmentId",
                table: "OutMaterials",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutMaterials_Persons_PersonId",
                table: "OutMaterials",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
