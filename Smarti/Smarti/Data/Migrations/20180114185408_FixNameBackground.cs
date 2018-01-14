using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Smarti.Data.Migrations
{
    public partial class FixNameBackground : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroungJobId",
                table: "TimeTasks");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundJobId",
                table: "TimeTasks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundJobId",
                table: "TimeTasks");

            migrationBuilder.AddColumn<string>(
                name: "BackgroungJobId",
                table: "TimeTasks",
                nullable: true);
        }
    }
}
