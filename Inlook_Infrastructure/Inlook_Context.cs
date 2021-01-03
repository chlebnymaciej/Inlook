using System;
using System.Collections.Generic;
using System.Text;
using Inlook_Core.Entities;
using Inlook_Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Inlook_Infrastructure
{
    public class Inlook_Context : DbContext
    {
        public Inlook_Context()
        {
        }

        public Inlook_Context(DbContextOptions<Inlook_Context> options)
            : base(options)
        {
        }

        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<Favorites> Favorites { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Mail> Mails { get; set; }

        public DbSet<MailTo> MailsTo { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserGroup> UserGroup { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.AddMockData();

            builder.ApplyConfiguration(new AttachmentConfiguration());
            builder.ApplyConfiguration(new FavoritesConfiguration());
            builder.ApplyConfiguration(new GroupConfiguration());
            builder.ApplyConfiguration(new MailConfiguration());
            builder.ApplyConfiguration(new MailToConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserGroupConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
