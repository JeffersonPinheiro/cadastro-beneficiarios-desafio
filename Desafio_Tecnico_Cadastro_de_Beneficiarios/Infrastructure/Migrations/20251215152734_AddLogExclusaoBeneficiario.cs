using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Migrations
{
    /// <inheritdoc />
    public partial class AddLogExclusaoBeneficiario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogsExclusaoBeneficiarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeneficiarioId = table.Column<int>(type: "int", nullable: false),
                    NomeBeneficiario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prioridade = table.Column<int>(type: "int", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsExclusaoBeneficiarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogsExclusaoBeneficiarios");
        }
    }
}
