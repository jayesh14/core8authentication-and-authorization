using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Model;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenService.GenerateToken(authClaims);

                // Generate a refresh token
                var refreshToken = Guid.NewGuid().ToString();

                // Store refresh token with the user
                await userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", refreshToken);

                return Ok(new
                {
                    token = token,
                    refreshToken = refreshToken
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        public async Task<bool> IsRefreshTokenValid(string refreshToken, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return false;
            }

            var storedToken = await userManager.GetAuthenticationTokenAsync(user, "MyApp", "RefreshToken");
            return storedToken == refreshToken;
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string accessToken, string refreshToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token.");
            }

            var username = principal.Identity?.Name;

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized();
            }

            var storedRefreshToken = await userManager.GetAuthenticationTokenAsync(user, "MyApp", "RefreshToken");

            if (storedRefreshToken != refreshToken)
            {
                return Unauthorized("Invalid refresh token.");
            }


            var newAccessToken = _tokenService.GenerateToken(principal.Claims);

            var newRefreshToken = Guid.NewGuid().ToString();

            // Update refresh token
            await userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", newRefreshToken);

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost("remove-token")]
        public async Task<IActionResult> RemoveToken(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Remove the stored refresh token
            await userManager.RemoveAuthenticationTokenAsync(user, "MyApp", "RefreshToken");

            return Ok("Token removed successfully.");
        }




    }
}
