using Microsoft.EntityFrameworkCore.Migrations;

namespace ProEventos.Persistence.Migrations
{
    public partial class campos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedeSociais_Eventos_EventoId",
                table: "RedeSociais");

            migrationBuilder.DropForeignKey(
                name: "FK_RedeSociais_Palestrantes_PalestranteId",
                table: "RedeSociais");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "RedeSociais",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "RedeSociais",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "ImagemURL",
                table: "Palestrantes",
                newName: "ImagemUrl");

            migrationBuilder.AddForeignKey(
                name: "FK_RedeSociais_Eventos_EventoId",
                table: "RedeSociais",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RedeSociais_Palestrantes_PalestranteId",
                table: "RedeSociais",
                column: "PalestranteId",
                principalTable: "Palestrantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedeSociais_Eventos_EventoId",
                table: "RedeSociais");

            migrationBuilder.DropForeignKey(
                name: "FK_RedeSociais_Palestrantes_PalestranteId",
                table: "RedeSociais");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "RedeSociais",
                newName: "URL");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "RedeSociais",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "ImagemUrl",
                table: "Palestrantes",
                newName: "ImagemURL");

            migrationBuilder.AddForeignKey(
                name: "FK_RedeSociais_Eventos_EventoId",
                table: "RedeSociais",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RedeSociais_Palestrantes_PalestranteId",
                table: "RedeSociais",
                column: "PalestranteId",
                principalTable: "Palestrantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
