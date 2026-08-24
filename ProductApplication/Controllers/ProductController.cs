using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApplication.Data;
using ProductApplication.Model;

namespace ProductApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("getAllProduct")]
        public async Task<IActionResult> getAllProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                if(products == null || products.Count == 0)
                {
                    return NotFound("No products found.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        { 
            try
            {
                var isRecordavilable = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id);
                if (isRecordavilable == null)
                {
                    return NotFound("Product not found.");
                }
                isRecordavilable.ProductName = product.ProductName;
                isRecordavilable.ProductPrice = product.ProductPrice;
                isRecordavilable.ProductDescription = product.ProductDescription;
                isRecordavilable.ProductCategory = product.ProductCategory;
                isRecordavilable.DateTime = product.DateTime;
                isRecordavilable.IsExpired = product.IsExpired;
                await _context.SaveChangesAsync();
                return Ok("Product updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var singleRecord = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id);
                if (singleRecord == null)
                {
                    return NotFound("Product not found.");
                }
                _context.Products.Remove(singleRecord);
                await _context.SaveChangesAsync();
                return Ok("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
