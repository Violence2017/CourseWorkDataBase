using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Migrations.RestarauntDb
{
    public partial class InitialRestarauntMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Customers",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Customers", x => x.Id); });

            migrationBuilder.CreateTable(
                "Employees",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: false),
                    Position = table.Column<string>(maxLength: 70, nullable: false),
                    Education = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Employees", x => x.Id); });

            migrationBuilder.CreateTable(
                "Providers",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Providers", x => x.Id); });

            migrationBuilder.CreateTable(
                "Orders",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    Payment = table.Column<int>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        "FK_Orders_Customers_CustomerId",
                        x => x.CustomerId,
                        "Customers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Orders_Employees_EmployeeId",
                        x => x.EmployeeId,
                        "Employees",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Ingredients",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    ExpirationDate = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        "FK_Ingredients_Providers_ProviderId",
                        x => x.ProviderId,
                        "Providers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Dishes",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    CookingTime = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                    table.ForeignKey(
                        "FK_Dishes_Orders_OrderId",
                        x => x.OrderId,
                        "Orders",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "DishIngredients",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(nullable: false),
                    DishId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishIngredients", x => x.Id);
                    table.ForeignKey(
                        "FK_DishIngredients_Dishes_DishId",
                        x => x.DishId,
                        "Dishes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_DishIngredients_Ingredients_IngredientId",
                        x => x.IngredientId,
                        "Ingredients",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Dishes_OrderId",
                "Dishes",
                "OrderId");

            migrationBuilder.CreateIndex(
                "IX_DishIngredients_DishId",
                "DishIngredients",
                "DishId");

            migrationBuilder.CreateIndex(
                "IX_DishIngredients_IngredientId",
                "DishIngredients",
                "IngredientId");

            migrationBuilder.CreateIndex(
                "IX_Ingredients_ProviderId",
                "Ingredients",
                "ProviderId");

            migrationBuilder.CreateIndex(
                "IX_Orders_CustomerId",
                "Orders",
                "CustomerId");

            migrationBuilder.CreateIndex(
                "IX_Orders_EmployeeId",
                "Orders",
                "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "DishIngredients");

            migrationBuilder.DropTable(
                "Dishes");

            migrationBuilder.DropTable(
                "Ingredients");

            migrationBuilder.DropTable(
                "Orders");

            migrationBuilder.DropTable(
                "Providers");

            migrationBuilder.DropTable(
                "Customers");

            migrationBuilder.DropTable(
                "Employees");
        }
    }
}