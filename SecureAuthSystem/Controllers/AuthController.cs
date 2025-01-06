using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureAuthSystem.Data;
using SecureAuthSystem.Models;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        JsonResponse json =  new();
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            return Ok(new { Status = "E", Message = "Email already exists" });

        user.PasswordHash = HashPassword(user.PasswordHash);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok(new { Status = "S", Message = "Register Successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var dbUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == user.Email);

        if (dbUser == null || !VerifyPassword(user.PasswordHash, dbUser.PasswordHash))
            return Ok(new{ Status = "E", Message = "Invalid credentials" });

        var token = JwtTokenHelper.GenerateToken(dbUser.Email, dbUser.Role, _configuration["Jwt:SecretKey"]);
        return Ok(new { Token = token, Status = "S",  Message = "Login Successfully" });
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private bool VerifyPassword(string password, string hashedPassword)
    {
        return HashPassword(password) == hashedPassword;
    }
}
