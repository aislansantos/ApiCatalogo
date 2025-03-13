using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class CategoriaSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Bebidas', 'bebidas.jpg')");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Lanches', 'lanches.jpg')");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Sobremesas', 'sobremesas.jpg')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {

        }
    }
}
