using AccessLayerDLL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AccessLayerDLL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<TaskListTemplate> TaskListTemplates { get; set; }
        public DbSet<TemplateGroup> TemplateGroups { get; set; }
        public DbSet<GroupTask> GroupTasks { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<TaskListTemplate>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<TemplateGroup>()
                .HasOne(tg => tg.TaskListTemplate)
                .WithMany(t => t.TemplateGroups)
                .HasForeignKey(tg => tg.TemplateID);

            builder.Entity<TemplateGroup>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<GroupTask>()
                .HasOne(g => g.TemplateGroups)
                .WithMany(t => t.GroupTasks)
                .HasForeignKey(g => g.GroupID);

            builder.Entity<GroupTask>()
                .HasOne(g => g.DependencyTask)
                .WithMany()
                .HasForeignKey(g => g.DependancyTaskID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<GroupTask>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
