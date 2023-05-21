using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ConversationInvitationConfiguration : IEntityTypeConfiguration<ConversationInvitation>
{
    public void Configure(EntityTypeBuilder<ConversationInvitation> builder)
    {
        builder.ToTable("ConversationInvitation");
        builder
            .HasKey(x => x.Id)
            .HasName("PK_ConversationInvitation_Id");
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
        builder
            .Property(x => x.Role)
            .HasDefaultValue(ConversationRole.Member);
        //* ref
        // Conversation
        builder
            .HasOne(x => x.Conversation)
            .WithMany(x => x.Invitations)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_ConversationInvitation_ConversationId");
        // User
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Invitations)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_ConversationInvitation_UserId");
        builder
            .HasOne(x => x.Creator)
            .WithMany(x => x.CreatedInvitations)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_ConversationInvitation_CreatedBy");
        builder
            .HasOne(x => x.Judger)
            .WithMany(x => x.JudgedInvitations)
            .HasForeignKey(x => x.JudgedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_ConversationInvitation_JudgedBy");
    }
}
