using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PRN221_GroupProject.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderandBirth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92cbe301-a2ce-416b-8408-136ce9ca3b18");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c341ba9f-22e2-4fd8-8530-faf86d9a71aa");

           /* migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "CartHeader");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Product_Category",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CartDetail",
                table: "CartDetail",
                newName: "CartDetailId");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Product_Category",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Product_CategoryId",
                table: "Product_Category",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(CONVERT([nvarchar](36),newid()))",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldDefaultValueSql: "(CONVERT([nvarchar](36),newid()))");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Product_Category",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Product_Category",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Product_Category",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Updatedby",
                table: "Product_Category",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Product",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "EmailTemplate",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Coupon",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Coupon",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Coupon",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Coupon",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Coupon",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Coupon",
                type: "datetime",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Category",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CartHeader",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CartHeader",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CartHeader",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CartHeader",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "CartHeader",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CartHeader",
                type: "datetime",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "CartDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "CartDetail",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CartDetail",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CartDetail",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "CartDetail",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CartDetail",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CartDetail",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");
*/
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            /*migrationBuilder.AddUniqueConstraint(
                name: "AK_Coupon_CouponId",
                table: "Coupon",
                column: "CouponId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CartHeader_CartId",
                table: "CartHeader",
                column: "CartId");

            migrationBuilder.CreateTable(
                name: "OrderHeader",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderHeaderId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CouponId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeader", x => x.id);
                    table.UniqueConstraint("AK_OrderHeader_OrderHeaderId", x => x.OrderHeaderId);
                    table.ForeignKey(
                        name: "FK_OrderHeader_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderHeader_Coupon",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "CouponId");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(CONVERT([nvarchar](36),newid()))"),
                    OrderHeaderId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_OrderHeader",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeader",
                        principalColumn: "OrderHeaderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Category_CreatedBy",
                table: "Product_Category",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreatedBy",
                table: "Product",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_createdBy",
                table: "EmailTemplate",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon",
                table: "Coupon",
                column: "CouponId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_CreatedBy",
                table: "Coupon",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreatedBy",
                table: "Category",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CartHeader",
                table: "CartHeader",
                column: "CartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartHeader_UserId",
                table: "CartHeader",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail",
                table: "CartDetail",
                column: "CartDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_CartId",
                table: "CartDetail",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_ProductId",
                table: "CartDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderHeaderId",
                table: "OrderDetail",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader",
                table: "OrderHeader",
                column: "OrderHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_CouponId",
                table: "OrderHeader",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_UserId",
                table: "OrderHeader",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_CartHeader",
                table: "CartDetail",
                column: "CartId",
                principalTable: "CartHeader",
                principalColumn: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Product",
                table: "CartDetail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartHeader_AspNetUsers",
                table: "CartHeader",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers",
                table: "Category",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupon_AspNetUsers",
                table: "Coupon",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplate_AspNetUsers",
                table: "EmailTemplate",
                column: "createdBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AspNetUsers",
                table: "Product",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_AspNetUsers",
                table: "Product_Category",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_CartHeader",
                table: "CartDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Product",
                table: "CartDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CartHeader_AspNetUsers",
                table: "CartHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Coupon_AspNetUsers",
                table: "Coupon");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplate_AspNetUsers",
                table: "EmailTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_AspNetUsers",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_AspNetUsers",
                table: "Product_Category");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "OrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_Product_Category_CreatedBy",
                table: "Product_Category");

            migrationBuilder.DropIndex(
                name: "IX_Product_CreatedBy",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_EmailTemplate_createdBy",
                table: "EmailTemplate");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Coupon_CouponId",
                table: "Coupon");

            migrationBuilder.DropIndex(
                name: "IX_Coupon",
                table: "Coupon");

            migrationBuilder.DropIndex(
                name: "IX_Coupon_CreatedBy",
                table: "Coupon");

            migrationBuilder.DropIndex(
                name: "IX_Category_CreatedBy",
                table: "Category");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CartHeader_CartId",
                table: "CartHeader");

            migrationBuilder.DropIndex(
                name: "IX_CartHeader",
                table: "CartHeader");

            migrationBuilder.DropIndex(
                name: "IX_CartHeader_UserId",
                table: "CartHeader");

            migrationBuilder.DropIndex(
                name: "IX_CartDetail",
                table: "CartDetail");

            migrationBuilder.DropIndex(
                name: "IX_CartDetail_CartId",
                table: "CartDetail");

            migrationBuilder.DropIndex(
                name: "IX_CartDetail_ProductId",
                table: "CartDetail");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Product_Category");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Product_Category");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Product_Category");

            migrationBuilder.DropColumn(
                name: "Updatedby",
                table: "Product_Category");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Coupon");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Coupon");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Coupon");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Coupon");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Coupon");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CartHeader");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CartHeader");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CartHeader");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CartHeader");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "CartDetail");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CartDetail");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CartDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CartDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CartDetail");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CartDetail");*/

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

           /* migrationBuilder.RenameColumn(
                name: "id",
                table: "Product_Category",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CartDetailId",
                table: "CartDetail",
                newName: "CartDetail");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Product_Category",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Product_CategoryId",
                table: "Product_Category",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "(CONVERT([nvarchar](36),newid()))",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldDefaultValueSql: "(CONVERT([nvarchar](36),newid()))");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Product",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "createdBy",
                table: "EmailTemplate",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Coupon",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Category",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CartHeader",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "CartHeader",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "CartHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "CartDetail",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
*/
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "92cbe301-a2ce-416b-8408-136ce9ca3b18", null, "admin", "admin" },
                    { "c341ba9f-22e2-4fd8-8530-faf86d9a71aa", null, "customer", "customer" }
                });
        }
    }
}
