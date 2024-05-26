using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PRN221_GroupProject.Migrations
{
    /// <inheritdoc />
    public partial class AddColumStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1ad25d1-4efb-4e84-bbb8-eded628d10ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd299bb5-a0b6-4de7-acc4-9f894c9cc8dc");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "92cbe301-a2ce-416b-8408-136ce9ca3b18", null, "admin", "admin" },
                    { "c341ba9f-22e2-4fd8-8530-faf86d9a71aa", null, "customer", "customer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92cbe301-a2ce-416b-8408-136ce9ca3b18");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c341ba9f-22e2-4fd8-8530-faf86d9a71aa");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f1ad25d1-4efb-4e84-bbb8-eded628d10ee", null, "admin", "admin" },
                    { "fd299bb5-a0b6-4de7-acc4-9f894c9cc8dc", null, "customer", "customer" }
                });
        }
    }
}
