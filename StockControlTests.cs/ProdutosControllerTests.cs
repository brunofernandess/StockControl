using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using StockControl.Controllers;
using StockControl.Data;
using StockControl.Models;

namespace StockControlTests.cs
{
    public class ProdutosControllerTests
    {
        // Inicializa o contexto do banco de dados
        private readonly StockControlDbContext _context;

        // Inicializa a controller de produtos
        private readonly ProdutosController _controller;

        public ProdutosControllerTests()
        {
            // Configura o contexto do banco de dados
            var options = new DbContextOptionsBuilder<StockControlDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new StockControlDbContext(options);

            // limpa o banco de dados antes de cada teste
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Configura a controller de produtos
            _controller = new ProdutosController(_context);
        }

        [Fact]
        public async Task GetAllProducts_Returns()
        {
            // arrange
            var products = new List<Product>
                {
                    new Product { Id = 1, Name = "Natura Essence", Marca = "Natura", CodigoProduto = "123", DataValidade = DateTime.UtcNow},
                    new Product { Id = 2, Name = "Natura Homem Sagaz", Marca = "Natura", CodigoProduto = "456", DataValidade = DateTime.UtcNow},
                    new Product { Id = 3, Name = "Quasar Vision", Marca = "Boticario", CodigoProduto = "789", DataValidade = DateTime.UtcNow}
                };


            // adiciona os produtos ao contexto do banco de dados
            _context.Products.AddRange(products);
             await _context.SaveChangesAsync(); 

            // act
            var result = await _controller.GetAllProducts();

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result); // verifica se o retorno é um objeto Ok
            var returnProducts = Assert.IsType<List<Product>>(okResult.Value); // verifica se o retorno é uma lista de produtos
            Assert.Equal(3, returnProducts.Count); // verifica se a lista de produtos contém 3 produtos
        }

        [Fact]
        public async Task GetProductsByFilter_Returns()
        {
            // arrange
            var products = new List<Product>
                {
                    new Product { Id = 1, Name = "Natura Essence", Marca = "Natura", CodigoProduto = "123", DataValidade = DateTime.UtcNow},
                    new Product { Id = 2, Name = "Natura Homem Sagaz", Marca = "Natura", CodigoProduto = "456", DataValidade = DateTime.UtcNow},
                    new Product { Id = 3, Name = "Quasar Vision", Marca = "Boticario", CodigoProduto = "789", DataValidade = DateTime.UtcNow}
                };

            // adiciona os produtos ao contexto do banco de dados
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            // act
            var result = await _controller.GetProductsByFilter("Natura", "123", DateTime.UtcNow);


            // assert
            var okResult = Assert.IsType<OkObjectResult>(result); // verifica se o retorno é um objeto Ok
            var returnProducts = Assert.IsType<List<Product>>(okResult.Value); // verifica se o retorno é uma lista de produtos
            Assert.Single(returnProducts); // verifica se a lista de produtos contém 1 produto
        }

        [Fact]

        public async Task AddProduct_Adds()
        {

            // arrange
            var newProduct = new Product
            {
                Name = "Quasar Brave",
                Marca = "Boticario",
                CodigoProduto = "101",
                DataValidade = DateTime.UtcNow
            };


            // act
            var result = await _controller.AddProduct(newProduct);
            
            // assert
            
            var createdResult = Assert.IsType<CreatedAtActionResult>(result); // verifica se o retorno é um objeto CreatedAtAction

            var returnProduct = Assert.IsType<Product>(createdResult.Value); // verifica se o retorno é um produto

            Assert.Equal(newProduct.Name, returnProduct.Name); // verifica se o nome do produto é igual ao nome do produto adicionado
            Assert.Equal(newProduct.Marca, returnProduct.Marca); // verifica se a marca do produto é igual à marca do produto adicionado
            Assert.Equal(newProduct.CodigoProduto, returnProduct.CodigoProduto); // verifica se o código do produto é igual ao código do produto adicionado
            Assert.Equal(newProduct.DataValidade, returnProduct.DataValidade); // verifica se a data de validade do produto é igual à data de validade do produto adicionado

            var productInDb = await _context.Products.FirstOrDefaultAsync(p => p.Id == returnProduct.Id); // busca o produto no banco de dados
        }

        [Fact]

        public async Task RemoveProduct_RemovesFromDatabase()
        {

            //arrange
            var product = new Product
            {
                Name = "Quasar Brave",
                Marca = "Boticario",
                CodigoProduto = "101",
                DataValidade = DateTime.UtcNow
            };

            // adiciona o produto ao contexto do banco de dados
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // verifica se o produto realmente existe no banco de dados
            var productExists = await _context.Products.AnyAsync(p => p.Id == product.Id);
            Assert.True(productExists);


            // act
            var result = await _controller.DeleteProduct(product.Id);

            // assert
            Assert.IsType<NoContentResult>(result);

            var productInDb = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id); // busca o produto no banco de dados
            Assert.Null(productInDb); // verifica se o produto não existe no banco de dados

        }
    }


}


