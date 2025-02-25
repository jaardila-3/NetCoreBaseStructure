using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Data.Migrations;

/// <inheritdoc />
public partial class AccountMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ROLES",
            columns: table => new
            {
                Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                Name = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                CreatedBy = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                UpdatedBy = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                IsDeleted = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ROLES", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "USERS",
            columns: table => new
            {
                Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                Name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                LastName = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                IdentificationNumber = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                Username = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                Dependency = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                Email = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                PasswordHash = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                LockoutEnd = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: false),
                AccessFailedCount = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                CreatedBy = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                UpdatedBy = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                IsDeleted = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_USERS", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "USER_ROLES",
            columns: table => new
            {
                Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                UserId = table.Column<string>(type: "CHAR(26)", nullable: false),
                RoleId = table.Column<string>(type: "CHAR(26)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                CreatedBy = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                UpdatedBy = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                IsDeleted = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_USER_ROLES", x => x.Id);
                table.ForeignKey(
                    name: "FK_USER_ROLES_ROLES_RoleId",
                    column: x => x.RoleId,
                    principalTable: "ROLES",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_USER_ROLES_USERS_UserId",
                    column: x => x.UserId,
                    principalTable: "USERS",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ROLES_CreatedAt",
            table: "ROLES",
            column: "CreatedAt");

        migrationBuilder.CreateIndex(
            name: "IX_ROLES_Name",
            table: "ROLES",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_USER_ROLES_CreatedAt",
            table: "USER_ROLES",
            column: "CreatedAt");

        migrationBuilder.CreateIndex(
            name: "IX_USER_ROLES_RoleId",
            table: "USER_ROLES",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_USER_ROLES_UserId_RoleId",
            table: "USER_ROLES",
            columns: new[] { "UserId", "RoleId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_USERS_CreatedAt",
            table: "USERS",
            column: "CreatedAt");

        migrationBuilder.CreateIndex(
            name: "IX_USERS_Email",
            table: "USERS",
            column: "Email",
            unique: true,
            filter: "\"Email\" IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_USERS_IdentificationNumber",
            table: "USERS",
            column: "IdentificationNumber",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_USERS_Username",
            table: "USERS",
            column: "Username",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "USER_ROLES");

        migrationBuilder.DropTable(
            name: "ROLES");

        migrationBuilder.DropTable(
            name: "USERS");
    }
}
