using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UserNameEmailUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3f563ae6-2424-4816-a1dc-00a103a50150"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Modified", "Password", "Status", "Type", "UserName" },
                values: new object[] { new Guid("2df87bb1-0fb0-4669-ab04-652a5974a2df"), "atur.aritonang@xsis.co.id", "Super", "User", new DateTime(2024, 3, 1, 14, 57, 9, 164, DateTimeKind.Local).AddTicks(1465), "5ce41ada64f1e8ffb0acfaafa622b141438f3a5777785e7f0b830fb73e40d3d6", 0, 1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Password",
                table: "Users",
                column: "Password",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Password",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2df87bb1-0fb0-4669-ab04-652a5974a2df"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Modified", "Password", "Status", "Type", "UserName" },
                values: new object[] { new Guid("3f563ae6-2424-4816-a1dc-00a103a50150"), "atur.aritonang@xsis.co.id", "Super", "User", new DateTime(2024, 2, 29, 13, 45, 11, 806, DateTimeKind.Local).AddTicks(3482), "5ce41ada64f1e8ffb0acfaafa622b141438f3a5777785e7f0b830fb73e40d3d6", 0, 1, "admin" });
        }
    }
}
