using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PipefittersSupply.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Financing");

            migrationBuilder.EnsureSchema(
                name: "HumanResources");

            migrationBuilder.EnsureSchema(
                name: "Purchasing");

            migrationBuilder.CreateTable(
                name: "CashDisbursementTypes",
                schema: "Financing",
                columns: table => new
                {
                    CashDisbursementTypeId = table.Column<int>(type: "int", nullable: false),
                    EventTypeName = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    PayeeTypeName = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(7)", nullable: true, defaultValueSql: "sysdatetime()"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    Id_Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashDisbursementTypes", x => x.CashDisbursementTypeId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                schema: "HumanResources",
                columns: table => new
                {
                    EmployeeTypeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeTypeName = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(7)", nullable: true, defaultValueSql: "sysdatetime()"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    Id_Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.EmployeeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                schema: "Purchasing",
                columns: table => new
                {
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId_Value = table.Column<int>(type: "int", nullable: true),
                    Id_Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PurchaseOrderId);
                });

            migrationBuilder.CreateTable(
                name: "TimeCards",
                schema: "HumanResources",
                columns: table => new
                {
                    TimeCardId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId_Value = table.Column<int>(type: "int", nullable: true),
                    SupervisorId_Value = table.Column<int>(type: "int", nullable: true),
                    Id_Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCards", x => x.TimeCardId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "HumanResources",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeType = table.Column<int>(type: "int", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    MiddleInitial = table.Column<string>(type: "nchar(1)", nullable: true),
                    SSN = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    StateProvinceCode = table.Column<string>(type: "nchar(2)", nullable: true),
                    Zipcode = table.Column<string>(type: "nvarchar(12)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(14)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nchar(1)", nullable: true),
                    TaxExemptions = table.Column<int>(type: "int", nullable: true),
                    PayRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(7)", nullable: true, defaultValueSql: "sysdatetime()"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    EmployeeTypeId = table.Column<int>(type: "int", nullable: true),
                    Id_Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                        column: x => x.EmployeeTypeId,
                        principalSchema: "HumanResources",
                        principalTable: "EmployeeTypes",
                        principalColumn: "EmployeeTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetails",
                schema: "Purchasing",
                columns: table => new
                {
                    PurchaseOrderDetailId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId_Value = table.Column<int>(type: "int", nullable: true),
                    PurchaseOrderId1 = table.Column<int>(type: "int", nullable: true),
                    Id_Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDetails", x => x.PurchaseOrderDetailId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_PurchaseOrders_PurchaseOrderId1",
                        column: x => x.PurchaseOrderId1,
                        principalSchema: "Purchasing",
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeTypeId",
                schema: "HumanResources",
                table: "Employees",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_PurchaseOrderId1",
                schema: "Purchasing",
                table: "PurchaseOrderDetails",
                column: "PurchaseOrderId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashDisbursementTypes",
                schema: "Financing");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetails",
                schema: "Purchasing");

            migrationBuilder.DropTable(
                name: "TimeCards",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "EmployeeTypes",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "PurchaseOrders",
                schema: "Purchasing");
        }
    }
}
