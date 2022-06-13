using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CGVakBooks.Migrations
{
    public partial class addProductTableDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    ListPrice = table.Column<double>(type: "double", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false),
                    Price50 = table.Column<double>(type: "double", nullable: false),
                    Price100 = table.Column<double>(type: "double", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    CategotyId = table.Column<int>(type: "int", nullable: false),
                    CoverTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

        }
    }
}
