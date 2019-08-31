using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodePassio_Web.Migrations
{
    public partial class ModifiedPostTagEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "PostTags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "PostTags",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "PostTags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "PostTags");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "PostTags");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "PostTags");
        }
    }
}
