using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using UMS.Application.DTOs;
using UMS.Domain.Models;
using UMS.Infrastructure.Abstraction.Services;
using UMS.Infrastructure.Services;

namespace UMS.WebApi.Controllers;

[Route("api")]
[ApiController]
public class AuthController : Controller
{
    private readonly IJwtService _jwtService;
    private readonly UmsContext _context;
     

        public AuthController( IJwtService jwtService,UmsContext context)
        {
            _jwtService = jwtService;
            _context = context;
        }
        
        
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users.Where(obj=>obj.Email==dto.Email).First();
            if (user == null)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }
            var jwtId = _jwtService.Generate(user.Id.ToString());
            Response.Cookies.Append("jwt", jwtId, new CookieOptions { HttpOnly = true });
            return Ok(jwtId);
        }

        [HttpGet()]
        public IActionResult GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                var user = _context.Users.Where(obj=>obj.Id==userId).First();
        
                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        
        [HttpGet("id")]
        public IActionResult GetUserId()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);
                return Ok(userId);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "Logout Success!" });
        }
}