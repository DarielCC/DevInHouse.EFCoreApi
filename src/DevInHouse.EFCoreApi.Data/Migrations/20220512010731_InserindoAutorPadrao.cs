using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevInHouse.EFCoreApi.Data.Migrations
{
    public partial class InserindoAutorPadrao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "Id", "Nome", "Sobrenome" },
                values: new object[] { 1, "João", "Pedro" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
