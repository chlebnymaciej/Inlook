﻿using Inlook_Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Infrastructure.Configurations
{
    public class FavoritesConfiguration : IEntityTypeConfiguration<Favorites>
    {
        public void Configure(EntityTypeBuilder<Favorites> builder)
        {
            builder.HasKey(f => new { f.UserId, f.FavoriteUserId });

            builder.HasOne(f => f.FavoriteUser)
                .WithMany(u => u.UsersThatFavorize)
                .HasForeignKey(f => f.FavoriteUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.User)
                .WithMany(u => u.FavoritesUsers)
                .HasForeignKey(f => f.UserId);
        }
    }
}
