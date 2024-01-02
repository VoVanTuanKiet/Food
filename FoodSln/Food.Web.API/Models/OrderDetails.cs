using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Web.API.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string OrderCode { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total{get;set;}
        [ForeignKey("ProductId")]
        public ProductModel products{get;set;}
    }
}
