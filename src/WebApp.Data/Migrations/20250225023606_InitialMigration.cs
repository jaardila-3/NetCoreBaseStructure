using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Data.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "LOGS",
            columns: table => new
            {
                Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                    .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                Timestamp = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                Level = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                Message = table.Column<string>(type: "NCLOB", nullable: false),
                Exception = table.Column<string>(type: "NCLOB", nullable: true),
                Properties = table.Column<string>(type: "NCLOB", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LOGS", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_LOGS_Timestamp",
            table: "LOGS",
            column: "Timestamp");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "LOGS");
    }
}
