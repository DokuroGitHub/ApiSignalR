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
            .HasKey(x => x.Id)
            .HasName("PK_Participant_Id");
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
            .WithMany(x => x.Participants)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Participant_ConversationId");
    }
}
