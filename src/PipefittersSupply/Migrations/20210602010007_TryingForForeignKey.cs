using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PipefittersSupply.Migrations
{
    public partial class TryingForForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupervisorId_Value",
                schema: "HumanResources",
                table: "Employees",
                newName: "SupervisorId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeTypes",
                type: "datetime2(7)",
                nullable: true,
                defaultValueSql: "sysdatetime()");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeTypeName",
                table: "EmployeeTypes",
                type: "nvarchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_Value",
                table: "EmployeeTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "EmployeeTypes",
                type: "datetime2(7)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                schema: "HumanResources",
                table: "Employees",
                type: "nvarchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                schema: "HumanResources",
                table: "Employees",
                type: "nvarchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "HumanResources",
                table: "Employees",
                type: "nvarchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "HumanResources",
                table: "Employees",
                type: "datetime2(7)",
                nullable: true,
                defaultValueSql: "sysdatetime()");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeTypeId",
                schema: "HumanResources",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "HumanResources",
                table: "Employees",
                type: "nvarchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "HumanResources",
                table: "Employees",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                schema: "HumanResources",
                table: "Employees",
                type: "datetime2(7)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "HumanResources",
                table: "Employees",
                type: "nvarchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                schema: "HumanResources",
                table: "Employees",
                type: "nchar(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleInitial",
                schema: "HumanResources",
                table: "Employees",
                type: "nchar(1)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PayRate",
                schema: "HumanResources",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SSN",
                schema: "HumanResources",
                table: "Employees",
                type: "nvarchar(11)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "HumanResources",
                table: "Employees",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateProvinceCode",
                schema: "HumanResources",
                table: "Employees",
                type: "nchar(2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaxExemptions",
                schema: "HumanResources",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                schema: "HumanResources",
                table: "Employees",
                type: "nvarchar(14)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zipcode",
                schema: "HumanResources",
                table: "Employees",
                type: "nvarchar(12)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "EmployeeTypes");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeName",
                table: "EmployeeTypes");

            migrationBuilder.DropColumn(
                name: "Id_Value",
                table: "EmployeeTypes");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "EmployeeTypes");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MiddleInitial",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PayRate",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SSN",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "StateProvinceCode",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TaxExemptions",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Telephone",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Zipcode",
                schema: "HumanResources",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                schema: "HumanResources",
                table: "Employees",
                newName: "SupervisorId_Value");
        }
    }
}
