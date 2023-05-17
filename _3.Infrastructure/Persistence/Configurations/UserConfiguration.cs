using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", x => x.HasCheckConstraint("CK_User_Money", "[Money] >= 0"));
        builder
            .HasKey(x => x.Id)
            .HasName("PK_User_Id");
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder
            .HasIndex(x => x.Email, "IX_User_Email");
        builder
            .Property(x => x.Username)
            .ValueGeneratedOnAdd();
        builder
            .HasIndex(x => x.Username, "IX_User_Username")
            .IsUnique();
        builder
            .Property(x => x.Money)
            .HasPrecision(18, 2);
        builder
            .Property(x => x.Role)
            .HasDefaultValue(UserRole.User);
        builder
            .Property(x => x.Role)
            .HasConversion(
                x => x.ToStringValue(),
                x => x.ToUserRole()
            );
        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
        // ghost
        builder
            .Property(x => x.DisplayName)
            .HasComputedColumnSql(@"
CASE
    WHEN [FirstName] IS NOT NULL AND [FirstName] <> ''
        THEN CASE
  			WHEN [LastName] IS NOT NULL AND [LastName] <> '' 
       			THEN [FirstName] + ' ' + [LastName]
       		ELSE [FirstName]
  		END
    WHEN [LastName] IS NOT NULL AND [LastName] <> '' THEN [LastName]
    WHEN [Email] IS NOT NULL AND [Email] <> '' THEN [Email]
	ELSE CAST([Id] AS varchar)
END", stored: false);
        // ref
        builder
            .HasOne(x => x.Creator)
            .WithMany(x => x.CreatedUsers)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientCascade);
        builder
            .HasMany(x => x.CreatedUsers)
            .WithOne(x => x.Creator)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_User_CreatedBy");
        builder
            .HasOne(x => x.Updater)
            .WithMany(x => x.UpdatedUsers)
            .HasForeignKey(x => x.UpdatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder
            .HasMany(x => x.UpdatedUsers)
            .WithOne(x => x.Updater)
            .HasForeignKey(x => x.UpdatedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_User_UpdatedBy");
        builder
            .HasOne(x => x.Manager)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.ManagerId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder
            .HasMany(x => x.Employees)
            .WithOne(x => x.Manager)
            .HasForeignKey(x => x.ManagerId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_User_ManagerId");
    }
}
