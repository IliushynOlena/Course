﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHelsi.Entities
{
    public class EFDbContext : IdentityDbContext<DbUser, DbRole, int, IdentityUserClaim<int>,
    DbUserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public DbSet<ListDoctors> Doctors { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public EFDbContext(DbContextOptions<EFDbContext> options)
            : base(options)
        {

        }
        // public DbSet<DbProduct> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DbUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}
