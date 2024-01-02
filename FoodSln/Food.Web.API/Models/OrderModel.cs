using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Food.Web.API.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public string UserName {  get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
    }
}
