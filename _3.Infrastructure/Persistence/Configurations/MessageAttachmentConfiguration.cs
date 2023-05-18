using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MessageAttachmentConfiguration : IEntityTypeConfiguration<MessageAttachment>
{
    public void Configure(EntityTypeBuilder<MessageAttachment> builder)
    {
        builder.ToTable("MessageAttachment");
        builder
            .HasKey(x => x.Id)
            .HasName("PK_MessageAttachment_Id");
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder
            .Property(x => x.Type)
            .HasDefaultValue(AttachmentType.File);
        // ref
        builder
            .HasOne(x => x.Message)
            .WithMany(x => x.Attachments)
            .HasForeignKey(x => x.MessageId)
            .HasPrincipalKey(x => x.Id)
            .HasConstraintName("FK_MessageAttachment_MessageId");
    }
}
