using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExampleAGAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImageUrl", "Metros", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Frasco de Mani", new DateTime(2023, 10, 24, 22, 5, 23, 155, DateTimeKind.Local).AddTicks(7524), new DateTime(2023, 10, 24, 22, 5, 23, 155, DateTimeKind.Local).AddTicks(7512), "", 50, "Mani", 5, 200.0 },
                    { 2, "", "Frasco de Almendra", new DateTime(2023, 10, 24, 22, 5, 23, 155, DateTimeKind.Local).AddTicks(7527), new DateTime(2023, 10, 24, 22, 5, 23, 155, DateTimeKind.Local).AddTicks(7526), "", 20, "Almendra", 15, 300.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
