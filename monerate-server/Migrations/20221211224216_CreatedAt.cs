using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace monerateserver.Migrations
{
    /// <inheritdoc />
    public partial class CreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RandomTable");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "RandomTable");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RandomTable",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RandomTable",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RandomTable");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RandomTable");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "RandomTable",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "RandomTable",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
