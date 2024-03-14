using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateModifiedByDanDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2df87bb1-0fb0-4669-ab04-652a5974a2df"));

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Modified", "Password", "Status", "Type", "UserName" },
                values: new object[] { new Guid("2df87bb1-0fb0-4669-ab04-652a5974a2df"), "atur.aritonang@xsis.co.id", "Super", "User", new DateTime(2024, 3, 1, 14, 57, 9, 164, DateTimeKind.Local).AddTicks(1465), "5ce41ada64f1e8ffb0acfaafa622b141438f3a5777785e7f0b830fb73e40d3d6", 0, 1, "admin" });
        }
    }
}
