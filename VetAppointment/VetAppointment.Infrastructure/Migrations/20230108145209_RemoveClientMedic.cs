using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetAppointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveClientMedic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Medics_MedicId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_MedicId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "MedicId",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MedicId",
                table: "Clients",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Clients_MedicId",
                table: "Clients",
                column: "MedicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Medics_MedicId",
                table: "Clients",
                column: "MedicId",
                principalTable: "Medics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
