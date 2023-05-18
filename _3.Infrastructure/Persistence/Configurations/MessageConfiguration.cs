using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Message");
        builder
            .HasKey(x => x.Id)
            .HasName("PK_Message_Id");
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
        // ref
        builder
            .HasOne(x => x.Conversation)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Message_ConversationId");
        builder
            .HasOne(x => x.LastMessageOfConversation)
            .WithOne(x => x.LastMessage)
            .HasForeignKey<Conversation>(x => x.LastMessageId)
            .HasPrincipalKey<Message>(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder
            .HasOne(x => x.ReplyToMessage)
            .WithMany(x => x.Replies)
            .HasForeignKey(x => x.ReplyTo)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Message_ReplyTo");
        builder
            .HasMany(x => x.Replies)
            .WithOne(x => x.ReplyToMessage)
            .HasForeignKey(x => x.ReplyTo)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
        builder
            .HasMany(x => x.DeletedMessages)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientCascade);
        builder
            .HasMany(x => x.Attachments)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientCascade);
        builder
            .HasMany(x => x.Emotes)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
