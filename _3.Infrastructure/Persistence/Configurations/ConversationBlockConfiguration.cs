using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ConversationBlockConfiguration : IEntityTypeConfiguration<ConversationBlock>
{
    public void Configure(EntityTypeBuilder<ConversationBlock> builder)
    {
        builder.ToTable("ConversationBlock");
        builder
            .HasKey(x => new {x.ConversationId,x.UserId, x.CreatedBy})
            .HasName("PK_ConversationBlock_ConversationId_UserId_CreatedBy");
        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
        // ref
        builder
            .HasOne(x => x.Conversation)
            .WithMany(x => x.Blocks)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_ConversationBlock_ConversationId");
    }
}
