using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaService.Api.Libro.Migrations
{
    /// <inheritdoc />
    public partial class MigrationLibro_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "libreriaMaterials",
                columns: table => new
                {
                    LibreriaMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AutorLibro = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_libreriaMaterials", x => x.LibreriaMaterialId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "libreriaMaterials");
        }
    }
}
