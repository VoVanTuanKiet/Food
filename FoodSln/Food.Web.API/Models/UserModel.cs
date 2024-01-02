using Microsoft.AspNetCore.Identity;

namespace Food.Web.API.Models
{
    public class UserModel : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
