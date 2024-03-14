using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookUp.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddModifireLookup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Attributes",
                columns: new[] { "Id", "ModifiedBy", "ModifiedDate", "Status", "Type", "Unit" },
                values: new object[] { new Guid("82d70ed1-23f7-4065-a141-7b2d0e2cf341"), null, null, 0, 0, "Unit" });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_Unit",
                table: "Attributes",
                column: "Unit",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attributes");
        }
    }
}
