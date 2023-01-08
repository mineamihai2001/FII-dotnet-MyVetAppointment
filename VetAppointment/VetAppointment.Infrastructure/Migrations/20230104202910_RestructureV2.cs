using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetAppointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestructureV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Clients_ClientId",
                table: "Bills");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "Bills",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Clients_ClientId",
                table: "Bills",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Clients_ClientId",
                table: "Bills");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "Bills",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Clients_ClientId",
                table: "Bills",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
