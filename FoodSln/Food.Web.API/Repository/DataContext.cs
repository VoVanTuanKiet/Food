using Food.Web.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Food.Web.API.Repository
{
    public class DataContext : IdentityDbContext<UserModel>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<ProductModel> Products =>Set<ProductModel>();
        public DbSet<CategoryModel> Categories =>Set<CategoryModel>();
        public DbSet<OrderModel> Orders => Set<OrderModel>();
        public DbSet<OrderDetails> OrderDetails => Set<OrderDetails>();



    }
}