using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureAuthSystem.Data;
using SecureAuthSystem.Models;

namespace SecureAuthSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Restrict access to Admin role
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        //[HttpPut("update-role/{id}")]
        //public async Task<IActionResult> UpdateRole(Guid id, [FromBody] string role)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null) return NotFound();

        //    user.Role = role;
        //    await _context.SaveChangesAsync();
        //    return Ok("Role updated");
        //}
    }
}
