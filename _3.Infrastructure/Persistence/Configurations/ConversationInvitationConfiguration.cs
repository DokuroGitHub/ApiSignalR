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
        // ref
        builder
            .HasOne(x => x.Conversation)
            .WithMany(x => x.Invitations)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_ConversationInvitation_ConversationId");
    }
}
