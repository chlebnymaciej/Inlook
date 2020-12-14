using Inlook_Core;
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
            Role adminRole = new Role() { Name = Roles.Admin, Id = Guid.NewGuid() };
            Role userRole = new Role() { Name = Roles.User, Id = Guid.NewGuid() };
            Role pendingRole = new Role() { Name = Roles.Pending, Id = Guid.NewGuid() };



            builder.Entity<Role>().HasData(
                adminRole,
                userRole,
                pendingRole
                );

            IList<User> users = new List<User>();
            users.Add(new User() { Name = "Stuart Burton",
                PhoneNumber = " + 48696969696", 
                Email = "polski@pingwin.pl", 
                Id = Guid.NewGuid() });

            User admin = new User()
            {
                Name = "Artur Chmura",
                PhoneNumber = " + 48696969696",
                Email = "artur.chmura3@op.pl",
                Id = new Guid("2884a694-6a60-4e87-9477-6bd589106ab2"),
                Accepted = true,
            };

            users.Add(admin);


            users.Add(new User() { Name = "Mariusz Pudzianowski",
                Email = "mariusz.pudzian@transport.pl",
                Id = Guid.NewGuid() });
            users.Add(new User() { Name = "Pan Paweł",
                Email = "mrpathix@elo.pl",
                Id = Guid.NewGuid() });
            users.Add(new User() { Name = "Janne Ahonen",
                Email = "nastepne@zawody.fi",
                Id = Guid.NewGuid() });
            users.Add(new User() { Name = "Jan Paweł",
                Email = "papiez_polak@vatican.vc",
                Id = Guid.NewGuid() });
            users.Add(new User() { Name = "Obi-Wan Kenobi",
                Email = "kenobi@jedi.order",
                Id = Guid.NewGuid() });
            users.Add(new User() { Name = "Palpatine",
                Email = "senat@sith.com",
                Id = Guid.NewGuid()
            });
            users.Add(new User() { Name = "Lech Wałęsa",
                Email = "plusydodatnie@soli.darnosc",
                Id = Guid.NewGuid()
            });


          

            builder.Entity<User>().HasData(users);

            IList<UserRole> userRoles = new List<UserRole>();

            userRoles.Add(new UserRole()
            {
                RoleId = adminRole.Id,
                UserId = admin.Id,
            });

            userRoles.Add(new UserRole()
            {
                RoleId = userRole.Id,
                UserId = admin.Id,
            });

            builder.Entity<UserRole>().HasData(userRoles);

        }
    }
}
