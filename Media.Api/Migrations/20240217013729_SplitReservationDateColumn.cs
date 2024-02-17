using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Media.Api.Migrations
{
    /// <inheritdoc />
    public partial class SplitReservationDateColumn : Migration
    {
        /// <inheritdoc />
        # pragma warning disable CA1062
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Reservations",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Reservations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        # pragma warning disable CA1062
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Reservations",
                newName: "Date");
        }
    }
}
