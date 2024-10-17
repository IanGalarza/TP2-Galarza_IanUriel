using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewClientSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientID", "Address", "Company", "CreateDate", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "1600 Amphitheatre Parkway, Mountain View, CA", "Google", new DateTime(2024, 9, 26, 19, 55, 9, 106, DateTimeKind.Local).AddTicks(7076), "pfernandez@google.com", "Pedro Fernandez", "555-123-4567" },
                    { 2, "410 Terry Ave N, Seattle, WA", "Amazon", new DateTime(2024, 9, 26, 19, 55, 9, 106, DateTimeKind.Local).AddTicks(7093), "mmartinez@amazon.com", "Martin Martinez", "555-987-6543" },
                    { 3, "One Microsoft Way, Redmond, WA", "Microsoft", new DateTime(2024, 9, 26, 19, 55, 9, 106, DateTimeKind.Local).AddTicks(7095), "aperez@microsoft.com", "Ana Perez", "555-456-7890" },
                    { 4, "2788 San Tomas Expressway, Santa Clara, CA", "Nvidia", new DateTime(2024, 9, 26, 19, 55, 9, 106, DateTimeKind.Local).AddTicks(7096), "lrodriguez@nvidia.com", "Lucas Rodriguez", "555-321-7654" },
                    { 5, "10400 NE 4th St, Bellevue, WA", "Valve", new DateTime(2024, 9, 26, 19, 55, 9, 106, DateTimeKind.Local).AddTicks(7097), "mbogado@valve.com", "Matias Bogado", "555-654-3210" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 5);
        }
    }
}
