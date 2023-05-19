using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.ToTable("Conversation");
        builder
            .HasKey(x => x.Id)
            .HasName("PK_Conversation_Id");
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
        // ref
        builder
            .HasOne(x => x.LastMessage)
            .WithOne(x => x.LastMessageOfConversation)
            .HasForeignKey<Conversation>(x => x.LastMessageId)
            .HasPrincipalKey<Message>(x => x.Id)
            .HasConstraintName("FK_Conversation_LastMessageId");
        builder
            .HasMany(x => x.Messages)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(x => x.Invitations)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(x => x.Blocks)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(x => x.Participants)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
