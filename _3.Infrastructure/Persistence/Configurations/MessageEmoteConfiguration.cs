using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MessageEmoteConfiguration : IEntityTypeConfiguration<MessageEmote>
{
    public void Configure(EntityTypeBuilder<MessageEmote> builder)
    {
        builder.ToTable("MessageEmote");
        builder
            .HasKey(x => new { x.MessageId, x.UserId })
            .HasName("PK_MessageEmote_MessageId_UserId");
        builder
            .Property(x => x.UpdatedAt)
            .HasDefaultValueSql("GETDATE()");
        builder
            .Property(x => x.Code)
            .HasDefaultValue(EmoteCode.Like);
        //* ref
        // Message
        builder
            .HasOne(x => x.Message)
            .WithMany(x => x.Emotes)
            .HasForeignKey(x => x.MessageId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_MessageEmote_MessageId");
        // User
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.MessageEmotes)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_MessageEmote_UserId");
    }
}
