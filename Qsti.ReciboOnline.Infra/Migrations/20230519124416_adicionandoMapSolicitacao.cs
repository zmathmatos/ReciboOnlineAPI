using Microsoft.EntityFrameworkCore.Migrations;

namespace Qsti.ReciboOnline.Infra.Migrations
{
    public partial class adicionandoMapSolicitacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Visivel",
                table: "Recibo",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Solicitacao",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Solicitacao");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Recibo",
                newName: "Visivel");
        }
    }
}
