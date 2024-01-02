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
    [Route("CategoryController")]

    public class CategoryController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;


        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(DataContext context, ILogger<CategoryController> logger,IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;

            _logger = logger;
        }
        [HttpGet(Name = "GetCategoryModel")]
        public IEnumerable<CategoryModel> Get()
        {
            var category = _dataContext.Categories.ToArray();
            var categories = category.Select(index => new CategoryModel
            {
                Name = index.Name,
                Slug = index.Slug,
                Description = index.Description,
                Status = index.Status,
                Id = index.Id,
            }).ToArray();

           
            return categories;
        }
       

    }
}
