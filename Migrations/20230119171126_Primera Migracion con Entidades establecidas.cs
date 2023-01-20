using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuLote.Migrations
{
    public partial class PrimeraMigracionconEntidadesestablecidas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinciaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provincias_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provincia_Id = table.Column<int>(type: "int", nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipios_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Municipios_Provincias_Provincia_Id",
                        column: x => x.Provincia_Id,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localidades",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipio_Id = table.Column<int>(type: "int", nullable: false),
                    LocalidadId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localidades_Localidades_LocalidadId",
                        column: x => x.LocalidadId,
                        principalTable: "Localidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Localidades_Municipios_Municipio_Id",
                        column: x => x.Municipio_Id,
                        principalTable: "Municipios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Barrios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Localidad_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barrios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Barrios_Localidades_Localidad_Id",
                        column: x => x.Localidad_Id,
                        principalTable: "Localidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metros = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Etapa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orientacion = table.Column<int>(type: "int", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Barrio_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lotes_Barrios_Barrio_Id",
                        column: x => x.Barrio_Id,
                        principalTable: "Barrios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lote_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tipos_Lotes_Lote_Id",
                        column: x => x.Lote_Id,
                        principalTable: "Lotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Barrios_Localidad_Id",
                table: "Barrios",
                column: "Localidad_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Localidades_LocalidadId",
                table: "Localidades",
                column: "LocalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Localidades_Municipio_Id",
                table: "Localidades",
                column: "Municipio_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_Barrio_Id",
                table: "Lotes",
                column: "Barrio_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_MunicipioId",
                table: "Municipios",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_Provincia_Id",
                table: "Municipios",
                column: "Provincia_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Provincias_ProvinciaId",
                table: "Provincias",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tipos_Lote_Id",
                table: "Tipos",
                column: "Lote_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipos");

            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "Barrios");

            migrationBuilder.DropTable(
                name: "Localidades");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "Provincias");
        }
    }
}
