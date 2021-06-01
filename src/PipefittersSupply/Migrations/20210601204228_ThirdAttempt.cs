using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PipefittersSupply.Migrations
{
    public partial class ThirdAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Financing");

            migrationBuilder.CreateTable(
                name: "CashDisbursementTypes",
                schema: "Financing",
                columns: table => new
                {
                    CashDisbursementTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventTypeName = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    PayeeTypeName = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2(7)", nullable: false, defaultValueSql: "sysdatetime()"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashDisbursementTypes", x => x.CashDisbursementTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashDisbursementTypes_EventTypeName",
                schema: "Financing",
                table: "CashDisbursementTypes",
                column: "EventTypeName",
                unique: true,
                filter: "[EventTypeName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CashDisbursementTypes_PayeeTypeName",
                schema: "Financing",
                table: "CashDisbursementTypes",
                column: "PayeeTypeName",
                unique: true,
                filter: "[PayeeTypeName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashDisbursementTypes",
                schema: "Financing");
        }
    }
}
