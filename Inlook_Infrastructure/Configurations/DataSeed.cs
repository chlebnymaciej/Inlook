using Inlook_Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Inlook_Infrastructure.Configurations
{
    static class DataSeed
    {
        public static void AddMockData(this ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role() { Name = "User", Id = Guid.NewGuid() },
                new Role() { Name = "Admin", Id = Guid.NewGuid() }
                );

            builder.Entity<User>().HasData(
              new User() { Name = "Stuart Burton" , PhoneNumber = " + 48696969696" , Email="polski@pingwin.pl", Id = Guid.NewGuid() },
              new User() { Name = "Mariusz Pudzianowski", Email = "mariusz.pudzian@transport.pl", Id = Guid.NewGuid() },
              new User() { Name = "Pan Paweł", Email = "mrpathix@elo.pl", Id = Guid.NewGuid() },
              new User() { Name = "Janne Ahonen", Email = "nastepne@zawody.fi", Id = Guid.NewGuid() }
              );
        }
    }
}
