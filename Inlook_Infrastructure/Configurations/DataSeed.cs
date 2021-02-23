using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Inlook_Core;
using Inlook_Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inlook_Infrastructure.Configurations
{
    internal static class DataSeed
    {
        public static void AddMockData(this ModelBuilder builder)
        {
            Role adminRole = new Role()
            {
                Id = new Guid("c55a9789-f0ab-4c32-aa78-f054a9e19a3f"),
                CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                Name = "Admin",
                Priority = 0
            };
            Role userRole = new Role()
            {
                Id = new Guid("2615313f-7df9-49bc-861a-2444abe24dcd"),
                CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                Name = "User",
                Priority = 0
            };
            Role pendingRole = new Role()
            {
                Id = new Guid("3ecaed79-15a1-43e8-89c7-3dafc33ae27d"),
                CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                Name = "Pending",
                Priority = 0
            };

            builder.Entity<Role>().HasData(
                adminRole,
                userRole,
                pendingRole);

            IList<User> users = new List<User>();
            users.Add(new User()
            {
                Name = "Stuart Burton",
                PhoneNumber = " + 48696969696",
                Email = "polski@pingwin.pl",
                Id = new Guid("83fe71d7-503b-4b40-b6b0-9f429394b822"),
            });

            User admin = new User()
            {
                Name = "Artur Chmura",
                PhoneNumber = " + 48696969696",
                Email = "artur.chmura3@op.pl",
                Id = new Guid("2884a694-6a60-4e87-9477-6bd589106ab2"),
            };

            User bogen = new User()
            {
                Name = "Maciej Chlebny",
                PhoneNumber = " + 4821372137",
                Email = "01142157@pw.edu.pl",
                Id = new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903"),
            };
            users.Add(admin);
            users.Add(bogen);

            users.Add(new User()
            {
                Name = "Mariusz Pudzianowski",
                Email = "mariusz.pudzian@transport.pl",
                Id = new Guid("958d9b13-ce78-453c-8287-e7a46720ae85"),
            });
            users.Add(new User()
            {
                Name = "Pan Paweł",
                Email = "mrpathix@elo.pl",
                Id = new Guid("a2a87bf8-02a3-40e8-a23b-ee199d970264"),
            });
            users.Add(new User()
            {
                Name = "Janne Ahonen",
                Email = "nastepne@zawody.fi",
                Id = new Guid("1ad37d42-f44f-4978-8e4f-9e37e95ad860"),
            });
            users.Add(new User()
            {
                Name = "Jan Paweł",
                Email = "papiez_polak@vatican.vc",
                Id = new Guid("76b7b060-58d4-4a79-b3b6-965576f204b4"),
            });
            users.Add(new User()
            {
                Name = "Obi-Wan Kenobi",
                Email = "kenobi@jedi.order",
                Id = new Guid("fbddf21d-4f5d-4eb9-9631-bb3d30235f64"),
            });
            users.Add(new User()
            {
                Name = "Palpatine",
                Email = "senat@sith.com",
                Id = new Guid("d8400a20-7282-4f86-a1e1-1385e8549ee2"),
            });
            users.Add(new User()
            {
                Name = "Lech Wałęsa",
                Email = "plusydodatnie@soli.darnosc",
                Id = new Guid("c8624a2f-f4cc-4885-8ea6-519cd78418c6"),
            });

            builder.Entity<User>().HasData(users);

            IList<UserRole> userRoles = new List<UserRole>();

            userRoles.Add(new UserRole()
            {
                UserId = new Guid("2884a694-6a60-4e87-9477-6bd589106ab2"),
                RoleId = new Guid("c55a9789-f0ab-4c32-aa78-f054a9e19a3f")
            });

            userRoles.Add(new UserRole()
            {
                UserId = new Guid("2884a694-6a60-4e87-9477-6bd589106ab2"),
                RoleId = new Guid("2615313f-7df9-49bc-861a-2444abe24dcd")
            });

            userRoles.Add(new UserRole()
            {
                UserId = new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903"),
                RoleId = new Guid("c55a9789-f0ab-4c32-aa78-f054a9e19a3f")
            });

            userRoles.Add(new UserRole()
            {
                UserId = new Guid("0d3a47cf-1cb3-4df4-a1b1-640a49b8b903"),
                RoleId = new Guid("2615313f-7df9-49bc-861a-2444abe24dcd")
            });
            builder.Entity<UserRole>().HasData(userRoles);
        }
    }
}
