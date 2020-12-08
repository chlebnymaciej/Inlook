using Inlook_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inlook_Infrastructure.Configurations
{
    public class MailToConfiguration : IEntityTypeConfiguration<MailTo>
    {
        public void Configure(EntityTypeBuilder<MailTo> builder)
        {
            builder.HasKey(m => new {m.MailId, m.RecipientId });

            builder.HasOne(m => m.Recipient)
                .WithMany(u => u.MailsReceived)
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Mail)
               .WithMany(m => m.Recipients)
               .HasForeignKey(m => m.MailId);
            builder.HasIndex(m => new { m.RecipientId, m.MailId });//??
        }
    }
}
