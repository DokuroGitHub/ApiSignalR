namespace Domain.Entities;

#pragma warning disable
public class User : BaseAuditableEntity
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public decimal Money { get; set; }
    public UserRole Role { get; set; }
    public int? ManagerId { get; set; }
    // ghost
    public string DisplayName { get; set; }
    // ref
    public virtual User Creator { get; set; }
    public virtual User? Updater { get; set; }
    public virtual User? Manager { get; set; }
    public virtual ICollection<User> CreatedUsers { get; set; }
    public virtual ICollection<User> UpdatedUsers { get; set; }
    public virtual ICollection<User> Employees { get; set; }
}
