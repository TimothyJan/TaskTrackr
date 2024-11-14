using Microsoft.EntityFrameworkCore;
using TaskTrackr.Server.Models;

namespace TaskTrackr.Server
{
    public class TaskTrackrDbContext : DbContext
    {
        public TaskTrackrDbContext(DbContextOptions<TaskTrackrDbContext> options) : base(options)
        {
        }

        // Define DbSets for each entity to represent database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }

        // Configure relationships and constraints using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
            });

            // Configure the Project entity
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ProjectId);
                entity.Property(e => e.ProjectName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);

                // Define one-to-many relationship with ProjectTasks
                entity.HasMany(e => e.ProjectTasks)
                      .WithOne()
                      .HasForeignKey(t => t.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure the ProjectTask entity
            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.HasKey(e => e.ProjectTaskId);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Progress).IsRequired().HasDefaultValue(0);

                // Define foreign key relationship with User (AssignedUserId)
                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(e => e.AssignedUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
