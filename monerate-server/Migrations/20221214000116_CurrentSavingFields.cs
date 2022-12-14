using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace monerateserver.Migrations
{
    /// <inheritdoc />
    public partial class CurrentSavingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "CurrentSaving",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "CurrentSaving",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "CurrentSaving");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "CurrentSaving");
        }
    }
}
