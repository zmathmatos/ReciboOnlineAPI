using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Qsti.ReciboOnline.Infra.Migrations
{
    public partial class relacaoFuncionarioRecibo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recibo_Funcionario_IdFuncionario",
                table: "Recibo");

            migrationBuilder.DropIndex(
                name: "IX_Recibo_IdFuncionario",
                table: "Recibo");

            migrationBuilder.DropColumn(
                name: "IdFuncionario",
                table: "Recibo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdFuncionario",
                table: "Recibo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recibo_IdFuncionario",
                table: "Recibo",
                column: "IdFuncionario");

            migrationBuilder.AddForeignKey(
                name: "FK_Recibo_Funcionario_IdFuncionario",
                table: "Recibo",
                column: "IdFuncionario",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
