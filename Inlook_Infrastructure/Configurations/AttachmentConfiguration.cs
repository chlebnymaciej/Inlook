using Inlook_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Infrastructure.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Mail)
                .WithMany(m => m.Attachments)
                .HasForeignKey(a => a.MailId);

            builder.Property(a => a.FilePath).HasMaxLength(255);
        }
    }
}
