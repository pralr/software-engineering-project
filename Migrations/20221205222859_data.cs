using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendaDeLanches.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataEnvioDaMensagem",
                table: "Mensagens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataEnvioDaMensagem",
                table: "Mensagens");
        }
    }
}
