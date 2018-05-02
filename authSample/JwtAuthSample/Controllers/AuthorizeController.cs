using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtAuthSample.Models;
using JwtAuthSample.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthSample.Controllers
{
    //[Route("Authorize/[controller]")]
    //[Authorize(Policy = "SuperAdminOnly")]
    public class AuthorizeController : Controller
    {
        private JwtSettings _jwtSettings;
        public AuthorizeController(IOptions<JwtSettings> _jwtSettingsAccesser)
        {
            _jwtSettings = _jwtSettingsAccesser.Value;
        }
        [HttpPost]
        public IActionResult Token([FromForm]LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!(viewModel.Password == "123456" && viewModel.Username == "我零0七"))
                {
                    return BadRequest();
                }

                var claims=new Claim[]
                {
                    new Claim(ClaimTypes.Role,"admin"), 
                    new Claim(ClaimTypes.Name,"王帅"), 
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims, DateTime.Now,
                    DateTime.Now.AddMinutes(30), creds);

                return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
            }

            return BadRequest();
        }
    }
}