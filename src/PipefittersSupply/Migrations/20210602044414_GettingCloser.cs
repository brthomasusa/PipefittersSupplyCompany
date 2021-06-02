using Microsoft.EntityFrameworkCore.Migrations;

namespace PipefittersSupply.Migrations
{
    public partial class GettingCloser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId1",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeTypeId1",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "EmployeeTypeId1",
                schema: "HumanResources",
                table: "Employees",
                newName: "EmployeeType");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeTypeId",
                schema: "HumanResources",
                table: "Employees",
                column: "EmployeeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                schema: "HumanResources",
                table: "Employees",
                column: "EmployeeTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "EmployeeTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeTypeId",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "EmployeeType",
                schema: "HumanResources",
                table: "Employees",
                newName: "EmployeeTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeTypeId1",
                schema: "HumanResources",
                table: "Employees",
                column: "EmployeeTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId1",
                schema: "HumanResources",
                table: "Employees",
                column: "EmployeeTypeId1",
                principalTable: "EmployeeTypes",
                principalColumn: "EmployeeTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
