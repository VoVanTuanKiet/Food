using Food.Web.API.Models;
using Food.Web.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Food.Web.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _dataContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ProductModel product)
        {
            string Slug = product.Name.Replace(" ", "-");
            var findPro = _dataContext.Products.Where(p => p.Slug == Slug);

            if (findPro != null)
            {
                return BadRequest("Already have in database");
            }
            else
            {
                CategoryModel category = _dataContext.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
                ProductModel products = new ProductModel
                {
                    Name = product.Name,
                    Image = product.Image,
                    Category = category,
                    CategoryId = product.CategoryId,
                    Price = product.Price,
                    Slug = product.Slug,
                    Description = product.Description,
                };
                _dataContext.Products.Add(products);
                _dataContext.SaveChanges();
                return Ok("Add product successful");
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ProductModel product)
        {
            if (id != product.ID)
            {
                return BadRequest("Invalid product ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await _dataContext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            // Kiểm tra slug
            product.Slug = product.Name.Replace(" ", "-");
            var existingSlugProduct = await _dataContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
            if (existingSlugProduct != null && existingSlugProduct.ID != id)
            {
                ModelState.AddModelError("", "Already have this in the database");
                return BadRequest(ModelState);
            }
            CategoryModel category = _dataContext.Categories.FirstOrDefault(c => c.Id == product.CategoryId);

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Slug = product.Slug;
            existingProduct.Category = category;
            existingProduct.Image = product.Image;

            _dataContext.Products.Update(existingProduct);
            await _dataContext.SaveChangesAsync();

            return Ok("Product updated successfully");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // if (!string.Equals(product.Image, "noimage.jpg"))
            // {
            //     string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Media/FoodImages");
            //     string oldFileImage = Path.Combine(uploadDir, product.Image);
            //     if (System.IO.File.Exists(oldFileImage))
            //     {
            //         System.IO.File.Delete(oldFileImage);
            //     }
            // }

            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();

            return Ok("Food has been deleted");
        }
        [HttpGet("SearchName")]
        public ActionResult SearchByName(string Search)
        {
            var product = _dataContext.Products.Include(p => p.Category).Where(p => p.Name.ToUpper().Contains(Search.ToUpper()));
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}