using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Food.Web.API.Models
{
    public class SignInModel
    {

        [DataType(DataType.Password),Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email"), EmailAddress]
        public string Email { get; set; }
    }
}
