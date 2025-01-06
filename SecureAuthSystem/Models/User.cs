using System.ComponentModel.DataAnnotations;

namespace SecureAuthSystem.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User"; // Default role

        public bool IsActive { get; set; } = true;
    }

    public class JsonResponse
    {
        public string? Status { get; set; } = "S";
        public string? Message { get; set; } = "Success";
        public object? Data { get; set; }
    }
}
