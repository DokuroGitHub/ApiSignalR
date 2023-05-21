using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DeletedMessageConfiguration : IEntityTypeConfiguration<DeletedMessage>
{
    public void Configure(EntityTypeBuilder<DeletedMessage> builder)
    {
        builder.ToTable("DeletedMessage");
        builder
            .HasKey(x => new { x.MessageId, x.CreatedBy })
            .HasName("PK_DeletedMessage_MessageId_CreatedBy");
        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
        //* ref
        // Message
        builder
            .HasOne(x => x.Message)
            .WithMany(x => x.DeletedMessages)
            .HasForeignKey(x => x.MessageId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_DeletedMessage_MessageId");
        // User
        builder
            .HasOne(x => x.Creator)
            .WithMany(x => x.CreatedDeletedMessages)
            .HasForeignKey(x => x.CreatedBy)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_DeletedMessage_CreatedBy");
    }
}
