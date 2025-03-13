using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class ProdutoSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
                   "VALUES('Coca-cola Diet','Refrigerante cola 350 ml','5.45','coca-cola.jpg', 50,now(),1)");
            mb.Sql("INSERT INTO produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
                   "VALUES('Lanche de Atum','Lanche de atum com maionese','8.50','atum.jpg', 10,now(),2)");
            mb.Sql("INSERT INTO produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
                   "VALUES('Pudim Pequeno','Pudim de leite condesado 100g','6.75','pudim.jpg', 20,now(),3)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {

        }
    }
}
