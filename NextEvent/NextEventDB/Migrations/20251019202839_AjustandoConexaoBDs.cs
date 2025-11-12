using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextEvent.Migrations
{
    /// <inheritdoc />
    public partial class AjustandoConexaoBDs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscricoes_Eventos_EventoId",
                table: "Inscricoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscricoes",
                table: "Inscricoes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Inscricoes");

            migrationBuilder.AlterColumn<int>(
                name: "InscricaoId",
                table: "Inscricoes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "EventoId",
                table: "Inscricoes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inscricoes",
                table: "Inscricoes",
                column: "InscricaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricoes_Eventos_EventoId",
                table: "Inscricoes",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscricoes_Eventos_EventoId",
                table: "Inscricoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscricoes",
                table: "Inscricoes");

            migrationBuilder.AlterColumn<int>(
                name: "EventoId",
                table: "Inscricoes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "InscricaoId",
                table: "Inscricoes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Inscricoes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inscricoes",
                table: "Inscricoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricoes_Eventos_EventoId",
                table: "Inscricoes",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id");
        }
    }
}
