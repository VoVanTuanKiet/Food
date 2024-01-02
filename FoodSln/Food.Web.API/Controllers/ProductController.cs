using Food.Web.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Food.Web.API.DTO;
using Food.Web.API.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Food.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;


        private readonly DataContext _dataContext;
        private readonly ILogger<ProductController> _logger;
        public ProductController(DataContext context, ILogger<ProductController> logger,IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;

            _logger = logger;
        }
        [HttpGet(Name = "GetProductModel")]
        public IEnumerable<ProductModel> Get()
        {
            var category = _dataContext.Categories.ToArray();
            var product = _dataContext.Products.ToArray();
            var categories = category.Select(index => new CategoryModel
            {
                Name = index.Name,
                Slug = index.Slug,
                Description = index.Description,
                Status = index.Status,
                Id = index.Id,
            });

            var products = product.Select(index => new ProductModel
            {
                ID = index.ID,
                Name = index.Name,
                CategoryId = index.CategoryId,
                Category = index.Category,
                Description = index.Description,
                Price = index.Price,
                Slug = index.Slug,
                Image = index.Image,
            })
            .ToArray();
            return products;
        }
       

    }
}
