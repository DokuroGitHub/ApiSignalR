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
        //* ref
        // User
        builder
            .HasOne(x => x.Creator)
            .WithMany(x => x.CreatedConversations)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Conversation_CreatedBy");
        builder
            .HasOne(x => x.Updater)
            .WithMany(x => x.UpdatedConversations)
            .HasForeignKey(x => x.UpdatedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Conversation_UpdatedBy");
        builder
            .HasOne(x => x.Deleter)
            .WithMany(x => x.DeletedConversations)
            .HasForeignKey(x => x.DeletedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_Conversation_DeletedBy");
        // Message
        builder
            .HasOne(x => x.LastMessage)
            .WithOne(x => x.LastMessageNoConversation)
            .HasForeignKey<Conversation>(x => x.LastMessageId)
            .HasPrincipalKey<Message>(x => x.Id)
            .HasConstraintName("FK_Conversation_LastMessageId");
        builder
            .HasMany(x => x.Messages)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        // ConversationInvitation
        builder
            .HasMany(x => x.Invitations)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        // ConversationBlock
        builder
            .HasMany(x => x.Blocks)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
        // Participant
        builder
            .HasMany(x => x.Participants)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
