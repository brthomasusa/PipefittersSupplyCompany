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
                    CashDisbursementTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                name: "Employees",
                schema: "HumanResources",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupervisorId_Value = table.Column<int>(type: "int", nullable: true),
                    Id_Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    EmployeeTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
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
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    TimeCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId_Value = table.Column<int>(type: "int", nullable: true),
                    SupervisorId_Value = table.Column<int>(type: "int", nullable: true),
                    Id_Value = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCards", x => x.TimeCardId);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetails",
                schema: "Purchasing",
                columns: table => new
                {
                    PurchaseOrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                name: "EmployeeTypes");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetails",
                schema: "Purchasing");

            migrationBuilder.DropTable(
                name: "TimeCards",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "PurchaseOrders",
                schema: "Purchasing");
        }
    }
}
