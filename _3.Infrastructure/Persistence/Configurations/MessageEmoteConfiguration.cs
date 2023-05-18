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
            .HasKey(x => x.Id)
            .HasName("PK_MessageEmote_Id");
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
        builder
            .Property(x => x.Code)
            .HasDefaultValue(EmoteCode.Like);
        // ref
        builder
            .HasOne(x => x.Message)
            .WithMany(x => x.Emotes)
            .HasForeignKey(x => x.MessageId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_MessageEmote_MessageId");
    }
}
