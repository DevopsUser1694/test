using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FunctionAppTest.Migrations
{
    /// <inheritdoc />
    public partial class initialsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "ProductName" },
                values: new object[,]
                {
                    { 1, "Chandan", new DateTime(2025, 2, 24, 23, 17, 53, 777, DateTimeKind.Local).AddTicks(8427), "Chandan", new DateTime(2025, 2, 24, 23, 17, 53, 777, DateTimeKind.Local).AddTicks(8443), "DefaultP1" },
                    { 2, "Amit", new DateTime(2025, 2, 24, 23, 17, 53, 777, DateTimeKind.Local).AddTicks(8480), "Amit", new DateTime(2025, 2, 24, 23, 17, 53, 777, DateTimeKind.Local).AddTicks(8481), "DefaultP2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
