using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Models.Entities;

namespace TaskManagement.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.AssignedTasks)
                .WithOne(t => t.Assignee)
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.ReportedTasks)
                .WithOne(t => t.Reporter)
                .HasForeignKey(t => t.ReporterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskItem>()
        .HasOne(t => t.Assignee)
        .WithMany(u => u.AssignedTasks)
        .HasForeignKey(t => t.AssigneeId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Reporter)
                .WithMany(u => u.ReportedTasks)
                .HasForeignKey(t => t.ReporterId)
                .OnDelete(DeleteBehavior.Restrict);


        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRoleClaim> RoleClaims { get; set; }
        public DbSet<ApplicationRole> Roles{ get; set; }
    }
}

