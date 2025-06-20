using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnboardingSIGDB1.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaFuncionarioCargo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Empresas_EmpresaId",
                table: "Funcionarios");

            migrationBuilder.CreateTable(
                name: "FuncionarioCargoVinculo",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(type: "int", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    DataVinculo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioCargoVinculo", x => new { x.FuncionarioId, x.CargoId });
                    table.ForeignKey(
                        name: "FK_FuncionarioCargoVinculo_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FuncionarioCargoVinculo_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuncionarioCargoVinculo_CargoId",
                table: "FuncionarioCargoVinculo",
                column: "CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Empresas_EmpresaId",
                table: "Funcionarios",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Empresas_EmpresaId",
                table: "Funcionarios");

            migrationBuilder.DropTable(
                name: "FuncionarioCargoVinculo");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Empresas_EmpresaId",
                table: "Funcionarios",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
