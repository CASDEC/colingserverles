using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coling.API.Afiliados.Migrations
{
    /// <inheritdoc />
    public partial class PrimeroAfiliados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ci = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaNacimiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    foto = table.Column<byte[]>(type: "varbinary(1)", maxLength: 1, nullable: true),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persona", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipoSocial",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombresocial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoSocial", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "afiliado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idpersona = table.Column<int>(type: "int", nullable: false),
                    fechaafiliacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    codigoafiliado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nrotituloprovisional = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_afiliado", x => x.id);
                    table.ForeignKey(
                        name: "FK_afiliado_persona_idpersona",
                        column: x => x.idpersona,
                        principalTable: "persona",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "direccion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idpersona = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_direccion", x => x.id);
                    table.ForeignKey(
                        name: "FK_direccion_persona_idpersona",
                        column: x => x.idpersona,
                        principalTable: "persona",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "telefono",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idpersona = table.Column<int>(type: "int", nullable: false),
                    nrotelefono = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_telefono", x => x.id);
                    table.ForeignKey(
                        name: "FK_telefono_persona_idpersona",
                        column: x => x.idpersona,
                        principalTable: "persona",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personatipoSocial",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idtiposocial = table.Column<int>(type: "int", nullable: false),
                    idpersona = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personatipoSocial", x => x.id);
                    table.ForeignKey(
                        name: "FK_personatipoSocial_persona_idpersona",
                        column: x => x.idpersona,
                        principalTable: "persona",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personatipoSocial_tipoSocial_idtiposocial",
                        column: x => x.idtiposocial,
                        principalTable: "tipoSocial",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "profesionafiliado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idafiliado = table.Column<int>(type: "int", nullable: false),
                    idprofesion = table.Column<int>(type: "int", nullable: false),
                    fechaasignacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    nrosellosib = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profesionafiliado", x => x.id);
                    table.ForeignKey(
                        name: "FK_profesionafiliado_afiliado_idafiliado",
                        column: x => x.idafiliado,
                        principalTable: "afiliado",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_afiliado_idpersona",
                table: "afiliado",
                column: "idpersona");

            migrationBuilder.CreateIndex(
                name: "IX_direccion_idpersona",
                table: "direccion",
                column: "idpersona");

            migrationBuilder.CreateIndex(
                name: "IX_personatipoSocial_idpersona",
                table: "personatipoSocial",
                column: "idpersona");

            migrationBuilder.CreateIndex(
                name: "IX_personatipoSocial_idtiposocial",
                table: "personatipoSocial",
                column: "idtiposocial");

            migrationBuilder.CreateIndex(
                name: "IX_profesionafiliado_idafiliado",
                table: "profesionafiliado",
                column: "idafiliado");

            migrationBuilder.CreateIndex(
                name: "IX_telefono_idpersona",
                table: "telefono",
                column: "idpersona");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "direccion");

            migrationBuilder.DropTable(
                name: "personatipoSocial");

            migrationBuilder.DropTable(
                name: "profesionafiliado");

            migrationBuilder.DropTable(
                name: "telefono");

            migrationBuilder.DropTable(
                name: "tipoSocial");

            migrationBuilder.DropTable(
                name: "afiliado");

            migrationBuilder.DropTable(
                name: "persona");
        }
    }
}
