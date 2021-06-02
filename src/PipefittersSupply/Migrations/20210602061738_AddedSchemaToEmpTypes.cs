using Microsoft.EntityFrameworkCore.Migrations;

namespace PipefittersSupply.Migrations
{
    public partial class AddedSchemaToEmpTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "EmployeeTypes",
                newName: "EmployeeTypes",
                newSchema: "HumanResources");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "EmployeeTypes",
                schema: "HumanResources",
                newName: "EmployeeTypes");
        }
    }
}
