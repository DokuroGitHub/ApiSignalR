namespace Domain.Entities;

#pragma warning disable
public class SampleUser : BaseAuditableEntity
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
    //* ref
    // SampleUser
    public virtual SampleUser Creator { get; set; }
    public virtual SampleUser? Updater { get; set; }
    public virtual SampleUser? Manager { get; set; }
    public virtual ICollection<SampleUser> CreatedUsers { get; set; }
    public virtual ICollection<SampleUser> UpdatedUsers { get; set; }
    public virtual ICollection<SampleUser> Employees { get; set; }
}
