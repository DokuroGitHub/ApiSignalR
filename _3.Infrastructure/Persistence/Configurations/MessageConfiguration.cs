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
        //* ref
        // Conversation
        builder
            .HasOne(x => x.Conversation)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Message_ConversationId");
        builder
            .HasOne(x => x.LastMessageNoConversation)
            .WithOne(x => x.LastMessage)
            .HasForeignKey<Conversation>(x => x.LastMessageId)
            .HasPrincipalKey<Message>(x => x.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);
        // User
        builder
            .HasOne(x => x.Creator)
            .WithMany(x => x.CreatedMessages)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Message_CreatedBy");
        builder
            .HasOne(x => x.Deleter)
            .WithMany(x => x.DeletedMessages)
            .HasForeignKey(x => x.DeletedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Message_DeletedBy");
        // Message
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
        // DeletedMessage
        builder
            .HasMany(x => x.DeletedMessages)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        // MessageAttachment
        builder
            .HasMany(x => x.Attachments)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        // MessageEmote
        builder
            .HasMany(x => x.Emotes)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
