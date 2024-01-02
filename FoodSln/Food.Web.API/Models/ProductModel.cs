using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Food.Web.API.Repository.Validation;

namespace Food.Web.API.Models
{

    public class ProductModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        public string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu cầu nhập mô tả")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập giá")]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        public string Slug { get; set; }
        public CategoryModel Category { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; } = "noimage.jpg";
        [NotMapped]
        [FileExtention]
        public IFormFile? ImageUpload { get; set; }
    }
}