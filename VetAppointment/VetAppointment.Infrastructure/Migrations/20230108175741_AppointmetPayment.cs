using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetAppointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AppointmetPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "Appointments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "Appointments");
        }
    }
}
