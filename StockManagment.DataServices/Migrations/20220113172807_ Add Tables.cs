using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagment.DataServices.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contract_In",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirmName = table.Column<string>(type: "TEXT", nullable: false),
                    ContractNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ContractSumma = table.Column<decimal>(type: "TEXT", nullable: false),
                    InSummaIntoContract = table.Column<decimal>(type: "TEXT", nullable: false),
                    OutSummaFromContract = table.Column<decimal>(type: "TEXT", nullable: false),
                    FrimPhone = table.Column<string>(type: "TEXT", nullable: false),
                    BankName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ContractDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract_In", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_In_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_In_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MockProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UnitType = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Factura_In",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContractId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FacturaNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    FacturaSumma = table.Column<decimal>(type: "TEXT", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura_In", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factura_In_Contract_In_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract_In",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Factura_In_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_In_UserId",
                table: "Contract_In",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_In_WarehouseId",
                table: "Contract_In",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_In_ContractId",
                table: "Factura_In",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_In_WarehouseId",
                table: "Factura_In",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factura_In");

            migrationBuilder.DropTable(
                name: "MockProducts");

            migrationBuilder.DropTable(
                name: "Contract_In");
        }
    }
}
