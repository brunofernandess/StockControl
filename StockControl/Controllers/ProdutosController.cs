using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockControl.Data;
using StockControl.Models;

namespace StockControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        // armazena a instância do contexto do banco de dados
        private readonly StockControlDbContext _context;

        public ProdutosController(StockControlDbContext context)
        {
            // inicializa o contexto do banco de dados
            _context = context;
        }


        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of products.</returns>eu j
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {

            try
            {
                // busca todos os produtos no banco de dados
                var products = await _context.Products.ToListAsync();

                // retorna a lista de produtos
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro aos buscar produtos: {ex.Message}");
            }
        }

        /// <summary>
        /// Filters products by brand, product code, and expiration date.
        /// </summary>
        /// <param name="marca">Brand of the product.</param>
        /// <param name="codigoProduto">Product code.</param>
        /// <param name="dataValidade">Expiration date.</param>
        /// <returns>A list of filtered products.</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetProductsByFilter(string? marca, string? codigoProduto, DateTime? dataValidade)
        {
            try
            {


                // cria uma query para buscar os produtos transformando em um objeto IQueryable permitindo a aplicação de forma dinâmica
                var query = _context.Products.AsQueryable();

                // verifica se os parâmetros de filtro foram informados
                if (!string.IsNullOrEmpty(marca))
                {
                    query = query.Where(p => p.Marca == marca);
                }

                if (!string.IsNullOrEmpty(codigoProduto))
                {
                    query = query.Where(p => p.CodigoProduto == codigoProduto);
                }

                if (dataValidade.HasValue)
                {
                    query = query.Where(p => p.DataValidade.Date == dataValidade.Value.Date);
                }

                // executa a query e retorna os produtos filtrados
                var products = await query.ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao filtrar produtos: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">Product to be added.</param>
        /// <returns>The added product.</returns>
        [HttpPost]

        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {

            if (product == null)
            {
                return BadRequest("Adicione um produto válido.");
            }

            try
            {
                // adiciona o produto ao dbcontext
                _context.Products.Add(product);

                // salva as alteraçõees no banco de dados de forma assíncrona 
                await _context.SaveChangesAsync();

                // retorna status 201 com os detalhes do produto adicionado
                return CreatedAtAction(nameof(GetAllProducts), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar produto: {ex.Message}");
            }
        }


        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">ID of the product to be deleted.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // busca produto pelo ID no banco de dados
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound("Produto não encontrado!");
            }

            try
            {
                // remove o produto do dbcontext
                _context.Products.Remove(product);

                // salva as alterações no bd de forma assíncrona
                await _context.SaveChangesAsync();

                // retorna status 204 (No Content) indicando que o produto foi removido
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar o produto: {ex.Message}");
            }
        }
    

    }
}
