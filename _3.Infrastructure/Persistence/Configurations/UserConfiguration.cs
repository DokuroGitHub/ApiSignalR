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
        builder.ToTable("User");
        builder
            .HasKey(x => x.Id)
            .HasName("PK_User_Id");
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder
            .Property(x => x.UserId)
            .ValueGeneratedOnAdd()
            .HasMaxLength(36);
        builder
            .HasIndex(x => x.UserId, "IX_User_UserId")
            .IsUnique();
        builder
            .Property(x => x.FirstName)
            .HasMaxLength(100);
        builder
            .Property(x => x.LastName)
            .HasMaxLength(100);
        builder
            .Property(x => x.Email)
            .HasMaxLength(100);
        builder
            .HasIndex(x => x.Email, "IX_User_Email");
        builder
            .Property(x => x.Username)
            .HasDefaultValueSql("NEWID()")
            .HasMaxLength(100);
        builder
            .HasIndex(x => x.Username, "IX_User_Username")
            .IsUnique();
        builder
            .Property(x => x.PasswordHash)
            .HasDefaultValueSql("NEWID()")
            .HasMaxLength(100);
        builder
            .Property(x => x.Role)
            .HasDefaultValue(UserRole.Anonymous)
            .HasMaxLength(10)
            .HasConversion(
                x => x.ToStringValue(),
                x => x.ToUserRole()
            );
        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()")
            .HasMaxLength(39);
        builder
            .Property(x => x.UpdatedAt)
            .HasMaxLength(39);
        // ghost
        builder
            .Property(x => x.DisplayName)
            .HasMaxLength(201)
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
        //* ref
        // User
        builder
            .HasOne(x => x.Creator)
            .WithMany(x => x.CreatedUsers)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(x => x.Updater)
            .WithMany(x => x.UpdatedUsers)
            .HasForeignKey(x => x.UpdatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder
            .HasMany(x => x.CreatedUsers)
            .WithOne(x => x.Creator)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_User_CreatedBy");
        builder
            .HasMany(x => x.UpdatedUsers)
            .WithOne(x => x.Updater)
            .HasForeignKey(x => x.UpdatedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_User_UpdatedBy");
        // Conversation
        builder
            .HasMany(x => x.CreatedConversations)
            .WithOne(x => x.Creator)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(x => x.UpdatedConversations)
            .WithOne(x => x.Updater)
            .HasForeignKey(x => x.UpdatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder
            .HasMany(x => x.DeletedConversations)
            .WithOne(x => x.Deleter)
            .HasForeignKey(x => x.DeletedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
        // ConversationBlock
        builder
            .HasMany(x => x.ConversationBlocks)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(x => x.CreatedConversationBlocks)
            .WithOne(x => x.Creator)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        // ConversationInvitation
        builder
            .HasMany(x => x.Invitations)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(x => x.CreatedInvitations)
            .WithOne(x => x.Creator)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(x => x.JudgedInvitations)
            .WithOne(x => x.Judger)
            .HasForeignKey(x => x.JudgedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
        // DeletedMessage
        builder
            .HasMany(x => x.CreatedDeletedMessages)
            .WithOne(x => x.Creator)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        // Message
        builder
            .HasMany(x => x.CreatedMessages)
            .WithOne(x => x.Creator)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(x => x.DeletedMessages)
            .WithOne(x => x.Deleter)
            .HasForeignKey(x => x.DeletedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        // MessageEmote
        builder
            .HasMany(x => x.MessageEmotes)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        // Participant
        builder
            .HasMany(x => x.Participants)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(x => x.CreatedParticipants)
            .WithOne(x => x.Creator)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(x => x.UpdatedParticipants)
            .WithOne(x => x.Updater)
            .HasForeignKey(x => x.UpdatedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder
            .HasMany(x => x.DeletedParticipants)
            .WithOne(x => x.Deleter)
            .HasForeignKey(x => x.DeletedBy)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
