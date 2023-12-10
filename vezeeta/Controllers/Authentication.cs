using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using vezeeta.DTO;

namespace vezeeta.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Patient")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly UserManager<User> usermanager;
        
        public Authentication(UserManager<User> usermanager)
        {
            this.usermanager = usermanager;
        }
        
        [HttpPost("register")] //api/account/register
        public async Task<IActionResult> Registration(RegisterUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                User user = new User();

                user.FirstName = userDTO.FirstName;
                user.LastName = userDTO.LastName;
                user.Email = userDTO.Email;
                user.PhoneNumber = userDTO.Phone;
                user.Gender = userDTO.Gender;
                user.DateOfBirth = userDTO.DateOfBirth;
                user.Image = userDTO.Image;
                user.UserName = userDTO.Username;

                IdentityResult result = await usermanager.CreateAsync(user, userDTO.Password);
                await usermanager.AddToRoleAsync(user, "Patient");

                if (result.Succeeded)
                {
                    return Ok("Account Added Successfully");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto) {
            if(ModelState.IsValid)
            {
                User user = await usermanager.FindByEmailAsync(userDto.Email);
                if (user != null) {
                    bool checkPass = await usermanager.CheckPasswordAsync(user, userDto.Password);
                    if(checkPass)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Email, user.Email));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var roles = await usermanager.GetRolesAsync(user);
                        foreach(var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("StrONGAutHENTICATIONKEy"));
                        SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: "https://localhost:7232/", 
                            audience: "https://localhost:4200/", 
                            claims: claims, 
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signingCredentials
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expiration = myToken.ValidTo
                        });
                    }
                    return Unauthorized();
                }
                return Unauthorized();
            }
            else
            {
                return Unauthorized();
            }
        
        }

        
    }
}
