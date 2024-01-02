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
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public CategoryController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoryModel category)
        {
            category.Slug = category.Name.Replace(" ", "-");
            var findCategory = _dataContext.Categories.Where(c => c.Slug == category.Slug).FirstOrDefault();
            if (findCategory != null)
            {
                return Ok("Already have in database");
            }
            else
            {

                CategoryModel categories = new CategoryModel
                {
                    Name = category.Name,
                    Description = category.Description,
                    Slug = category.Slug,
                    Status = category.Status,
                };
                _dataContext.Categories.Add(categories);
                _dataContext.SaveChanges();
                return Ok(category);
            }
        }

        //delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CategoryModel category = await _dataContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();

            return Ok("Category has been deleted");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(CategoryModel categories, int id)
        {
            if (id != categories.Id)
            {
                return BadRequest("Invalid product ID");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCategory = await _dataContext.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            // Kiểm tra slug
            categories.Slug = categories.Name.Replace(" ", "-");
            var existingSlugCategory = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Slug == categories.Slug);
            if (existingCategory != null && existingCategory.Id != id)
            {
                ModelState.AddModelError("", "Already have this in the database");
                return BadRequest(ModelState);
            }

            existingCategory.Name = categories.Name;
            existingCategory.Description = categories.Description;
            existingCategory.Status = categories.Status;
            existingCategory.Slug = categories.Slug;


            _dataContext.Categories.Update(existingCategory);
            await _dataContext.SaveChangesAsync();

            return Ok("Category updated successfully");
        }
    }


}