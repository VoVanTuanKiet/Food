using Food.Web.API.Models;


using Microsoft.AspNetCore.Identity;

namespace Food.Web.API.Repository
{    
   
    public interface IAccountRepository
    {
        public Task<IdentityResult> SingUpAsync(SignUpModel model);
        public Task<string> SingInAsync(SignInModel model);
    }
}
