using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Qsti.ReciboOnline.Infra.Migrations
{
    public partial class atualizandoTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<Guid>(
                name: "SolicitacaoId",
                table: "Recibo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recibo_SolicitacaoId",
                table: "Recibo",
                column: "SolicitacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recibo_Solicitacao_SolicitacaoId",
                table: "Recibo",
                column: "SolicitacaoId",
                principalTable: "Solicitacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recibo_Solicitacao_SolicitacaoId",
                table: "Recibo");

            migrationBuilder.DropIndex(
                name: "IX_Recibo_SolicitacaoId",
                table: "Recibo");

            migrationBuilder.DropColumn(
                name: "SolicitacaoId",
                table: "Recibo");

            migrationBuilder.AlterColumn<string>(
                name: "Mes",
                table: "Solicitacao",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 7,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ano",
                table: "Solicitacao",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 7,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReciboId",
                table: "Solicitacao",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_ReciboId",
                table: "Solicitacao",
                column: "ReciboId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacao_Recibo_ReciboId",
                table: "Solicitacao",
                column: "ReciboId",
                principalTable: "Recibo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
