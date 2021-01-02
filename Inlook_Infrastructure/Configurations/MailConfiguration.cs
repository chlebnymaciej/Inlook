using Inlook_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inlook_Infrastructure.Configurations
{
    public class MailConfiguration : IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(m => m.Sender)
                .WithMany(u => u.MailsSend)
                .HasForeignKey(m => m.SenderId);

            builder.Property(m => m.Subject).HasMaxLength(100);

            builder.HasIndex(m => m.Id);
        }
    }
}
