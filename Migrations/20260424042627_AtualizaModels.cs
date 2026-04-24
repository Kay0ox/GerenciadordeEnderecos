using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuSiteEmMVC.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Usuarios",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuarios",
                newName: "ID");
        }
    }
}
