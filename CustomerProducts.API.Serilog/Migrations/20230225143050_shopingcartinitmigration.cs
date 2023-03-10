using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerProducts.API.SqlServer.Migrations
{
    public partial class shopingcartinitmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"), "Leeds", "Ali", "Umair" },
                    { new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "Manchester", "Shahzad", "Ahmed" },
                    { new Guid("2aadd2df-7caf-45ab-9355-7f6332985a87"), "Hull", "Junaid", "Masood" },
                    { new Guid("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"), "Manchester", "Saleem", "Anwar" },
                    { new Guid("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"), "London", "Sumair", "Rana" },
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Bradford", "Abdullah", "Awais" },
                    { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "Leeds", "Abulhadi", "Ali" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CustomerId", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "Asus Laptop 32 GB memory with 500GB hard disk.", "Asus Laptop" },
                    { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "HP Laptop 16 GB memory with 500GB hard disk", "HP Laptop" },
                    { new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "Plantronic.", "Headset" },
                    { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Microsoft.", "Keyboard" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId",
                table: "Products",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
