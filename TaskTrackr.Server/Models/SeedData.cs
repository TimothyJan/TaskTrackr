using Microsoft.EntityFrameworkCore;
using TaskTrackr.Server.Models;

namespace TaskTrackr.Server
{
    public static class SeedData
    {
        public static void ApplySeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Alice Johnson", Email = "alice@example.com", Role = "Manager" },
                new User { UserId = 2, Name = "Bob Smith", Email = "bob@example.com", Role = "Developer" },
                new User { UserId = 3, Name = "Charlie Brown", Email = "charlie@example.com", Role = "Tester" }
            );

            // Seed Projects
            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, ProjectName = "Project Alpha", Description = "First project", Status = "Active" },
                new Project { ProjectId = 2, ProjectName = "Project Beta", Description = "Second project", Status = "Completed" }
            );

            // Seed Project Tasks with correct ProjectId and assign to users
            modelBuilder.Entity<ProjectTask>().HasData(
                new ProjectTask
                {
                    ProjectTaskId = 1,
                    ProjectId = 1, // Task belongs to Project Alpha
                    Title = "Task 1",
                    Description = "Task for Project Alpha",
                    Status = "Not Started",
                    Progress = 0,
                    AssignedUserId = 2 // Bob Smith
                },
                new ProjectTask
                {
                    ProjectTaskId = 2,
                    ProjectId = 1, // Task belongs to Project Alpha
                    Title = "Task 2",
                    Description = "Another Task for Project Alpha",
                    Status = "In Progress",
                    Progress = 50,
                    AssignedUserId = 3 // Charlie Brown
                },
                new ProjectTask
                {
                    ProjectTaskId = 3,
                    ProjectId = 2, // Task belongs to Project Beta
                    Title = "Task 3",
                    Description = "Task for Project Beta",
                    Status = "Completed",
                    Progress = 100,
                    AssignedUserId = 1 // No assigned user
                },
                new ProjectTask
                {
                    ProjectTaskId = 4,
                    ProjectId = 2, // Task belongs to Project Beta
                    Title = "Task 4",
                    Description = "Another Task for Project Beta",
                    Status = "In Progress",
                    Progress = 75,
                    AssignedUserId = 1 // Alice Johnson
                }
            );
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new TaskTrackrDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TaskTrackrDbContext>>());

            if (context.Database.EnsureCreated())
            {
                // Database was created; no additional steps needed
            }
        }
    }
}
