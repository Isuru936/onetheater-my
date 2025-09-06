using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneTheater.Modules.Users.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_ContactInfo : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "contact_infos",
            schema: "users",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                phone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                address = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_contact_infos", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_contact_infos_user_id",
            schema: "users",
            table: "contact_infos",
            column: "user_id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "contact_infos",
            schema: "users");
    }
}
