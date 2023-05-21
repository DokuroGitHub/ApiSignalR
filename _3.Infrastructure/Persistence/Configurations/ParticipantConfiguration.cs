using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.ToTable("Participant");
        builder
            .HasKey(x => new { x.ConversationId, x.UserId })
            .HasName("PK_Participant_ConversationId_UserId");
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
            .WithMany(x => x.Participants)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Participant_ConversationId");
        // User
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Participants)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Participant_UserId");
        builder
            .HasOne(x => x.Creator)
            .WithMany(x => x.CreatedParticipants)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Participant_CreatedBy");
        builder
            .HasOne(x => x.Updater)
            .WithMany(x => x.UpdatedParticipants)
            .HasForeignKey(x => x.UpdatedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Participant_UpdatedBy");
        builder
            .HasOne(x => x.Deleter)
            .WithMany(x => x.DeletedParticipants)
            .HasForeignKey(x => x.DeletedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Participant_DeletedBy");
    }
}
