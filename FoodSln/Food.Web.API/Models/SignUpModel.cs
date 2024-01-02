using System.ComponentModel.DataAnnotations;

namespace Food.Web.API.Models
{
    public class SignUpModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập mật khẩu")]

        public string Password { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
