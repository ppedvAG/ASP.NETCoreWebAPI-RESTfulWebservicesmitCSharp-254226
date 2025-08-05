using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessModel.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomerImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("090595dc-676e-e76b-f10d-46d827baed27"),
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("15ea8044-ee27-80ee-d264-51253577094e"),
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("1a8767d6-b385-a886-af72-7663a7813769"),
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("548191ca-7b35-ea0a-92b1-fa06747c8eb9"),
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("6ecc5d17-098b-5714-9ab0-36cfd6280bb3"),
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("75cf9b42-3880-beb5-a060-0bf84a173efd"),
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("96ba173e-04ae-3bcd-9986-9e56f0adbf3a"),
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("e0a84cc0-053f-6b84-6734-5a650c36d1d8"),
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("f211ae0d-9ad5-44b0-9503-49af28861196"),
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("fd8929bf-5100-9fc6-e4e0-f50e71052d7e"),
                column: "ImageUrl",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Customers");
        }
    }
}
