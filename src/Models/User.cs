namespace ProductApi.src.Models;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string GoogleId { get; set; } = string.Empty;
    public DateTime? EmailVerifiedAt { get; set; }
    public string Status { get; set; } = "active";
    public string RememberToken { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}