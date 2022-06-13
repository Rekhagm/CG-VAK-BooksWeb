using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGVakBooks.Migrations
{
    public partial class addShoppingCartTableDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_CoverType_CoverTypeId",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoverType",
                table: "CoverType");

            migrationBuilder.RenameTable(
                name: "CoverType",
                newName: "covertype");

            migrationBuilder.AddPrimaryKey(
                name: "PK_covertype",
                table: "covertype",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ProductId",
                table: "ShoppingCart",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_covertype_CoverTypeId",
                table: "products",
                column: "CoverTypeId",
                principalTable: "covertype",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_covertype_CoverTypeId",
                table: "products");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_covertype",
                table: "covertype");

            migrationBuilder.RenameTable(
                name: "covertype",
                newName: "CoverType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoverType",
                table: "CoverType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_CoverType_CoverTypeId",
                table: "products",
                column: "CoverTypeId",
                principalTable: "CoverType",
                principalColumn: "Id");
        }
    }
}
