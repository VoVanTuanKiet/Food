using Food.Web.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Food.Web.API.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
			_context.Database.Migrate();
			if (!_context.Products.Any())
			{
				CategoryModel rice = new CategoryModel { Name = "cơm", Slug = "com", Description = "cơm thanh long", Status = 1 };
				CategoryModel noodle = new CategoryModel { Name = "my", Slug = "my-xao", Description = "mỳ thanh long", Status = 1 };
				_context.Products.AddRange(
					new ProductModel { Name = "com chiên", Slug = "com-chien", Description = "cơm chiên nhà làm", Image = "1.jpg", Category = rice, Price = 10000 },
					new ProductModel { Name = "mỳ xào", Slug = "my-xao", Description = "mỳ thanh long xào 1 trứng", Image = "2.jpg", Category = noodle, Price = 1000 }
					);
				_context.SaveChanges();
			}
		}
    }
}