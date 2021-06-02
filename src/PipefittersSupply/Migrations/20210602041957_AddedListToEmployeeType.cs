using Microsoft.EntityFrameworkCore.Migrations;

namespace PipefittersSupply.Migrations
{
    public partial class AddedListToEmployeeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeTypeId1",
                schema: "HumanResources",
                table: "Employees",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId1",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeTypeId1",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId1",
                schema: "HumanResources",
                table: "Employees");
        }
    }
}
