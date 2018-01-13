using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Smarti.Data.Migrations
{
    public partial class TypeToAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "TimeTasks");

            migrationBuilder.AddColumn<bool>(
                name: "Action",
                table: "TimeTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "TimeTasks");

            migrationBuilder.AddColumn<bool>(
                name: "Type",
                table: "TimeTasks",
                nullable: false,
                defaultValue: false);
        }
    }
}
