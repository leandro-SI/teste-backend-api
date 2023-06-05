using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestao.Data.Migrations
{
    public partial class addDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pessoas",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tarefas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    data_inicio = table.Column<DateTime>(nullable: true),
                    data_fim = table.Column<DateTime>(nullable: false),
                    duracao_estimada = table.Column<TimeSpan>(nullable: false),
                    situacao = table.Column<int>(nullable: false),
                    pessoa_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tarefas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tarefas_pessoas_pessoa_id",
                        column: x => x.pessoa_id,
                        principalTable: "pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "arquivos",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    extensao = table.Column<string>(nullable: true),
                    caminho = table.Column<string>(nullable: true),
                    tarefa_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arquivos", x => x.id);
                    table.ForeignKey(
                        name: "FK_arquivos_tarefas_tarefa_id",
                        column: x => x.tarefa_id,
                        principalTable: "tarefas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "pessoas",
                columns: new[] { "id", "nome" },
                values: new object[] { 1L, "João Silva" });

            migrationBuilder.InsertData(
                table: "pessoas",
                columns: new[] { "id", "nome" },
                values: new object[] { 2L, "Ana Silva" });

            migrationBuilder.CreateIndex(
                name: "IX_arquivos_tarefa_id",
                table: "arquivos",
                column: "tarefa_id");

            migrationBuilder.CreateIndex(
                name: "IX_tarefas_pessoa_id",
                table: "tarefas",
                column: "pessoa_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "arquivos");

            migrationBuilder.DropTable(
                name: "tarefas");

            migrationBuilder.DropTable(
                name: "pessoas");
        }
    }
}
