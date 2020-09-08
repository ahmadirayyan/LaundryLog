using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LaundryLog.Web.Migrations
{
    public partial class UpdateBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "LauUnit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "LauUnit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "LauLog",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "LauLog",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "LauItem",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "LauItem",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "LauUnit");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "LauUnit");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "LauLog");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "LauLog");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "LauItem",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "LauItem",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
