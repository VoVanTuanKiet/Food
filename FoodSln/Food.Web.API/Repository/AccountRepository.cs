using Food.Web.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Food.Web.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole>roleManager;
        public AccountRepository(UserManager<UserModel> userManager,SignInManager<UserModel>signInManager,IConfiguration configuration,RoleManager<IdentityRole>roleManager)
        { 
            this.userManager = userManager;
            this.signInManager=signInManager;
            this.configuration=configuration;
            this.roleManager=roleManager;
        }

        public async Task<string> SingInAsync(SignInModel model)
        {
            var user=await userManager.FindByEmailAsync(model.Email);
            var passwordValid=await userManager.CheckPasswordAsync(user,model.Password);
            if(user==null||!passwordValid)
            {
                return string.Empty;
            }

            var authClaims=new List<Claim>
            {
                new Claim(ClaimTypes.Email,model.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var userRoles=await userManager.GetRolesAsync(user);
            foreach(var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role,role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValueIssuer"],
                audience: configuration["JWT:ValueAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims:authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SingUpAsync(SignUpModel model)
        {
            var user = new UserModel
            {
                FirstName=model.FirstName,
                LastName=model.LastName,
                UserName=model.Email,
                Email=model.Email,        
            };
            var result= await userManager.CreateAsync(user,model.Password);

            if(result.Succeeded)
            {
                if(!await roleManager.RoleExistsAsync(AppRole.Client))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Client));
                }
                await userManager.AddToRoleAsync(user,AppRole.Client);
            }
            return result;
        }
    }
}
