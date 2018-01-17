using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Smarti.Data.Migrations
{
    public partial class DeviceIdLengthFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "Sockets",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "Sockets",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14,
                oldNullable: true);
        }
    }
}
