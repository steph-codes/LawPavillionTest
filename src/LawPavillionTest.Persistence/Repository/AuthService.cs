using LawPavillionTest.Core.Models;
using LawPavillionTest.Domain.DTOs.Request;
using LawPavillionTest.Domain.DTOs.Response;
using LawPavillionTest.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LawPavillionTest.Persistence.Repository
{
    public class AuthService : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly LawPavillionTestContext _context;
        public AuthService(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        LawPavillionTestContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        // Implement registration logic
        async Task<Response> IAuthRepository.RegisterAsync(RegisterModel request)
        {
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)   return   new Response { Status = "400", Message = "Email already exists!" };

            var currentuser = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber);
            if (currentuser != null)  return new Response { Status = "400", Message = "phone number  already exists!" };
          
    
            IdentityUser user = new()
            {
                //Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber

            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new Response { Status = "404", Message = "User creation failed! Please Use a stronger password, Include numbers and underscores." };

            return new Response { Status = "200", Message = "User created successfully!" };
            

            
        }

        async Task<LoginResponse> IAuthRepository.LoginAsync(LoginModel request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            
                return new LoginResponse
                {
                    Response = new Response { Status = "404", Message = "User does not exist, Check your details or Sign up to get Started" }
                };

                //check if user has valid password
                var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

                if (!userHasValidPassword)
                    return new LoginResponse
                    {
                        Response = new Response { Status = "400", Message = "Wrong Password,Please enter the correct password or Reset Password" }
                    };

                if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
                {

                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GetToken(authClaims);

                    return new LoginResponse
                    {
                        Username = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Response = new Response { Status = "200", Message = "User authenticated successfully!" }

                    };
                }
                return new LoginResponse
                {
                    Response = new Response { Status = "404", Message = "Please Check Credentials and Try again!" }

                };
            
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
