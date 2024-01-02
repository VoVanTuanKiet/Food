using Food.Web.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Food.Web.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Orders")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public CheckoutController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult>GetOrders()
        {
            var orders= await _dataContext.Orders.OrderByDescending(o=>o.Id).ToListAsync();
            return Ok(orders);
        }
         [HttpGet("Details")]
        public async Task<IActionResult>GetOrder()
        {
            var orders= await _dataContext.OrderDetails.OrderByDescending(o=>o.Id).ToListAsync();
            return Ok(orders);
        }
        [HttpGet("{ordercode}")]
        public async Task<IActionResult>ViewOrder(string ordercode)
        {
            var detailsorder=await _dataContext.OrderDetails.Include(od=>od.products).Where(od=>od.OrderCode==ordercode).ToListAsync();
            return Ok(detailsorder);
        }
    }
}