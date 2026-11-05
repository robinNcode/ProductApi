using Microsoft.EntityFrameworkCore;
using ProductApi.src.Models;
using Microsoft.AspNetCore.Mvc;
using ProductApi.src.Data;

namespace ProductApi.src.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    // Add your authentication methods here (e.g., login, register, etc.)
    // GET: api/users or api/users?userId=5
    [HttpGet("users")]
    public IActionResult GetUser(int? userId = null)
    {
        if (userId.HasValue && userId.Value > 0){
            var user = _context.Users.FirstOrDefault(u => u.Id == userId.Value);
            if (user == null){
                return NotFound();
            }
            return Ok(user);
        }else{
            var users = _context.Users.ToList();
            if (users == null || users.Count == 0){
                return NotFound();
            }
            return Ok(users);
        }
    }
}

