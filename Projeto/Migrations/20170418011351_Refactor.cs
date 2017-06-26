using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Projeto.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cpf = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Tipo = table.Column<short>(nullable: false),
                    Usuario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chamado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<int>(nullable: false),
                    DataChamadoAbertura = table.Column<DateTimeOffset>(nullable: false),
                    DataChamadoFechamento = table.Column<DateTimeOffset>(nullable: false),
                    Mensagem = table.Column<string>(nullable: true),
                    Respondido = table.Column<bool>(nullable: false),
                    RespostaAtendente = table.Column<string>(nullable: true),
                    RespostaCliente = table.Column<string>(nullable: true),
                    Situacao = table.Column<short>(nullable: false),
                    Tipo = table.Column<short>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Urgencia = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chamado_Pessoa_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChamadoId = table.Column<int>(nullable: false),
                    Mensagem = table.Column<string>(nullable: true),
                    Nota = table.Column<short>(nullable: false),
                    Resolvido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Chamado_ChamadoId",
                        column: x => x.ChamadoId,
                        principalTable: "Chamado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chamado_ClienteId",
                table: "Chamado",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ChamadoId",
                table: "Feedback",
                column: "ChamadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Chamado");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
