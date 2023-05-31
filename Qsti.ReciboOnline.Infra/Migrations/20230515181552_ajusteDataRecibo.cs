using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Qsti.ReciboOnline.Infra.Migrations
{
    public partial class ajusteDataRecibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    Administrador = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recibo",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 14, nullable: false),
                    Ano = table.Column<string>(nullable: false),
                    Mes = table.Column<string>(nullable: false),
                    Matricula = table.Column<string>(nullable: true),
                    TipoFolha = table.Column<int>(nullable: false),
                    CNPJ = table.Column<string>(maxLength: 14, nullable: true),
                    RazaoSocial = table.Column<string>(maxLength: 40, nullable: true),
                    Logradouro = table.Column<string>(maxLength: 50, nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    Cidade = table.Column<string>(maxLength: 20, nullable: true),
                    UF = table.Column<string>(maxLength: 2, nullable: true),
                    Nome = table.Column<string>(maxLength: 40, nullable: true),
                    Funcao = table.Column<string>(maxLength: 40, nullable: true),
                    CentroCusto = table.Column<string>(maxLength: 25, nullable: true),
                    ContaCorrente = table.Column<string>(maxLength: 25, nullable: true),
                    CodigoPd = table.Column<int>(nullable: false),
                    DescricaoCodigoPD = table.Column<string>(maxLength: 50, nullable: true),
                    Referencia = table.Column<string>(maxLength: 20, nullable: true),
                    Valor = table.Column<double>(maxLength: 20, nullable: false),
                    TipoCodigo = table.Column<string>(maxLength: 1, nullable: true),
                    DescricaoTipo = table.Column<string>(maxLength: 20, nullable: true),
                    SalarioBase = table.Column<double>(maxLength: 20, nullable: false),
                    TotalProventos = table.Column<double>(maxLength: 20, nullable: false),
                    TotalDescontos = table.Column<double>(maxLength: 20, nullable: false),
                    TotalLiquido = table.Column<double>(maxLength: 20, nullable: false),
                    FGTSMes = table.Column<double>(maxLength: 20, nullable: false),
                    BaseFGTS = table.Column<double>(maxLength: 20, nullable: false),
                    BaseINSS = table.Column<double>(maxLength: 20, nullable: false),
                    BaseIR = table.Column<double>(maxLength: 20, nullable: false),
                    BaseIRFerias = table.Column<double>(maxLength: 20, nullable: false),
                    OrdemTipo = table.Column<int>(maxLength: 20, nullable: false),
                    Visivel = table.Column<bool>(nullable: false),
                    Visualizado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recibo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Senha = table.Column<string>(maxLength: 36, nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UsuarioUltimaAlteracaoId = table.Column<Guid>(nullable: true),
                    DataUltimaMudancaStatus = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Usuario_UsuarioUltimaAlteracaoId",
                        column: x => x.UsuarioUltimaAlteracaoId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Matricula = table.Column<string>(maxLength: 10, nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Senha = table.Column<string>(maxLength: 255, nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DataUltimaMudancaStatus = table.Column<DateTime>(nullable: false),
                    UsuarioDesbloqueioId = table.Column<Guid>(nullable: true),
                    DataBloqueio = table.Column<DateTime>(nullable: true),
                    UsuarioBloqueioId = table.Column<Guid>(nullable: true),
                    DataDesbloqueio = table.Column<DateTime>(nullable: true),
                    Motivo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionario_Usuario_UsuarioBloqueioId",
                        column: x => x.UsuarioBloqueioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Funcionario_Usuario_UsuarioDesbloqueioId",
                        column: x => x.UsuarioDesbloqueioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Solicitacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: true),
                    ReciboId = table.Column<Guid>(nullable: true),
                    Ano = table.Column<string>(nullable: false),
                    Mes = table.Column<string>(nullable: false),
                    TipoFolha = table.Column<int>(maxLength: 1, nullable: false),
                    Matricula = table.Column<string>(maxLength: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitacao_Recibo_ReciboId",
                        column: x => x.ReciboId,
                        principalTable: "Recibo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitacao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PushNotification",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdUsuario = table.Column<Guid>(nullable: true),
                    FuncionarioId = table.Column<Guid>(nullable: true),
                    Token = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PushNotification_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PushNotification_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_UsuarioBloqueioId",
                table: "Funcionario",
                column: "UsuarioBloqueioId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_UsuarioDesbloqueioId",
                table: "Funcionario",
                column: "UsuarioDesbloqueioId");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotification_FuncionarioId",
                table: "PushNotification",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotification_IdUsuario",
                table: "PushNotification",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_ReciboId",
                table: "Solicitacao",
                column: "ReciboId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_UsuarioId",
                table: "Solicitacao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UsuarioUltimaAlteracaoId",
                table: "Usuario",
                column: "UsuarioUltimaAlteracaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "PushNotification");

            migrationBuilder.DropTable(
                name: "Solicitacao");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Recibo");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
