using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExampleAGAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "NumeroProductos",
                columns: table => new
                {
                    NroProducto = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Detalles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroProductos", x => x.NroProducto);
                    table.ForeignKey(
                        name: "FK_NumeroProductos_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroProductos_IdProducto",
                table: "NumeroProductos",
                column: "IdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroProductos");

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImageUrl", "Metros", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Frasco de Mani", new DateTime(2023, 10, 24, 22, 5, 23, 155, DateTimeKind.Local).AddTicks(7524), new DateTime(2023, 10, 24, 22, 5, 23, 155, DateTimeKind.Local).AddTicks(7512), "", 50, "Mani", 5, 200.0 },
                    { 2, "", "Frasco de Almendra", new DateTime(2023, 10, 24, 22, 5, 23, 155, DateTimeKind.Local).AddTicks(7527), new DateTime(2023, 10, 24, 22, 5, 23, 155, DateTimeKind.Local).AddTicks(7526), "", 20, "Almendra", 15, 300.0 }
                });
        }
    }
}
