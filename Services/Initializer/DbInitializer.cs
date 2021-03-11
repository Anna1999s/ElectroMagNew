using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces.Initializer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    if (!context.Roles.Any())
                    {
                        var roles = new List<IdentityRole>();
                        roles.Add(new IdentityRole
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });
                        roles.Add(new IdentityRole
                        {
                            Name = "Moderator",
                            NormalizedName = "MODERATOR",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });
                        roles.Add(new IdentityRole
                        {
                            Name = "Seller",
                            NormalizedName = "SELLER",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });
                        roles.Add(new IdentityRole
                        {
                            Name = "Buyer",
                            NormalizedName = "BUYER",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });
                        foreach (var role in roles)
                            context.Roles.Add(role);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}