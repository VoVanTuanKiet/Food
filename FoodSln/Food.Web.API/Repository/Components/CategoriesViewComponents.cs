using Food.Web.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food.Web.API.Repository.Component
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly DataContext _datacontext;
        public CategoriesViewComponent(DataContext context)
        {
            _datacontext=context;
        }
        public  async Task<IViewComponentResult> InvokeAsync()=>View(await _datacontext.Categories.ToListAsync());
    }
}