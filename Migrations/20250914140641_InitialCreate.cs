using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalDeVehiculosUltimaVersion.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    primerNombre = table.Column<string>(type: "varchar(90)", unicode: false, maxLength: 90, nullable: false),
                    segundoNombre = table.Column<string>(type: "varchar(90)", unicode: false, maxLength: 90, nullable: true),
                    primerApellido = table.Column<string>(type: "varchar(90)", unicode: false, maxLength: 90, nullable: false),
                    segundoApellido = table.Column<string>(type: "varchar(90)", unicode: false, maxLength: 90, nullable: true),
                    ci = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    numeroContacto = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    email = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    contrasenia = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    estado = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1, comment: "Inactivo = 0,\r\nActivo = 1,"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ultimaActualizacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cliente_Usuario",
                        column: x => x.id,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    direccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    salarioBasico = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    cargo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.id);
                    table.ForeignKey(
                        name: "FK_Empleado_Usuario",
                        column: x => x.id,
                        principalTable: "Usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCliente = table.Column<int>(type: "int", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    divisa = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    descuento = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    estado = table.Column<byte>(type: "tinyint", nullable: false, comment: "0 = Pendiente,\r\n1 = Pagado,\r\n2 = Anulado,\r\n3 = Reembolsado"),
                    ultimaActualizacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pago_Cliente",
                        column: x => x.idCliente,
                        principalTable: "Cliente",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    marca = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    modelo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    placa = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    kilometraje = table.Column<int>(type: "int", nullable: false),
                    capacidadMotor = table.Column<int>(type: "int", nullable: false),
                    combustible = table.Column<byte>(type: "tinyint", nullable: false, comment: "Gasolina = 0,\r\nDiesel = 1,\r\nHibrido = 2,\r\nElectrico = 3"),
                    transmision = table.Column<byte>(type: "tinyint", nullable: false, comment: "Manual = 0, \r\nAutomática = 1, \r\nCVT = 2"),
                    idCliente = table.Column<int>(type: "int", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ultimaActualizacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.id);
                    table.ForeignKey(
                        name: "FK_Vehiculo_Cliente",
                        column: x => x.idCliente,
                        principalTable: "Cliente",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AdministradorDeProgramacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministradorDeProgramacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_AdministradorDeProgramacion_Empleado",
                        column: x => x.id,
                        principalTable: "Empleado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AdministradorDeRepuestos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministradorDeRepuestos", x => x.id);
                    table.ForeignKey(
                        name: "FK_AdministradorDeRepuestos_Empleado",
                        column: x => x.id,
                        principalTable: "Empleado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Mantenimiento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVehiculo = table.Column<int>(type: "int", nullable: false),
                    fechaProgramada = table.Column<DateTime>(type: "datetime", nullable: false),
                    fechaEjecucion = table.Column<DateTime>(type: "datetime", nullable: true),
                    estado = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1, comment: "Cancelado =0,\r\nProgramado = 1\r\nEn Progreso =2,\r\nCompletado = 3,"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ultimaActualizacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    idAdminMantenimiento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimiento", x => x.id);
                    table.ForeignKey(
                        name: "FK_Mantenimiento_AdministradorDeProgramacion",
                        column: x => x.idAdminMantenimiento,
                        principalTable: "AdministradorDeProgramacion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Mantenimiento_Vehiculo",
                        column: x => x.idVehiculo,
                        principalTable: "Vehiculo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "InventarioDeRepuestos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idAdminRepuestos = table.Column<int>(type: "int", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ultimaActualizacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioDeRepuestos", x => x.id);
                    table.ForeignKey(
                        name: "FK_InventarioDeRepuestos_AdministradorDeRepuestos",
                        column: x => x.idAdminRepuestos,
                        principalTable: "AdministradorDeRepuestos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "MantenimientoPago",
                columns: table => new
                {
                    idMantenimiento = table.Column<int>(type: "int", nullable: true),
                    idPago = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_MantenimientoPago_Mantenimiento",
                        column: x => x.idMantenimiento,
                        principalTable: "Mantenimiento",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_MantenimientoPago_Pago",
                        column: x => x.idPago,
                        principalTable: "Pago",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    precio = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    idMantenimiento = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1, comment: "Cancelado = 0,\r\nEn Espera = 1,\r\nEn progreso = 2,\r\nFinalizado = 3"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    lastUpdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.id);
                    table.ForeignKey(
                        name: "FK_Servicio_Mantenimiento",
                        column: x => x.idMantenimiento,
                        principalTable: "Mantenimiento",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Repuestos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    precio = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    stockActual = table.Column<int>(type: "int", nullable: false),
                    stockMinimo = table.Column<int>(type: "int", nullable: false),
                    idInventario = table.Column<int>(type: "int", nullable: false),
                    estadoDeUso = table.Column<byte>(type: "tinyint", nullable: false, comment: "Nuevo = 1,\r\nUsado = 0,\r\n"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ultimaActualizacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repuestos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Repuestos_InventarioDeRepuestos",
                        column: x => x.idInventario,
                        principalTable: "InventarioDeRepuestos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventarioDeRepuestos_idAdminRepuestos",
                table: "InventarioDeRepuestos",
                column: "idAdminRepuestos");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_idAdminMantenimiento",
                table: "Mantenimiento",
                column: "idAdminMantenimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_idVehiculo",
                table: "Mantenimiento",
                column: "idVehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_MantenimientoPago_idMantenimiento",
                table: "MantenimientoPago",
                column: "idMantenimiento");

            migrationBuilder.CreateIndex(
                name: "IX_MantenimientoPago_idPago",
                table: "MantenimientoPago",
                column: "idPago");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_idCliente",
                table: "Pago",
                column: "idCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Repuestos_idInventario",
                table: "Repuestos",
                column: "idInventario");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_idMantenimiento",
                table: "Servicio",
                column: "idMantenimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_idCliente",
                table: "Vehiculo",
                column: "idCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MantenimientoPago");

            migrationBuilder.DropTable(
                name: "Repuestos");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "InventarioDeRepuestos");

            migrationBuilder.DropTable(
                name: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "AdministradorDeRepuestos");

            migrationBuilder.DropTable(
                name: "AdministradorDeProgramacion");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
