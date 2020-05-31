using System;

namespace PMS.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using PMS.Domain;

    namespace Db
    {
        public class PmsContext : DbContext
        {
            public PmsContext(DbContextOptions options)
                : base(options)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }

            public DbSet<Project> Project { get; set; }
            public DbSet<TaskState> TaskState { get; set; }
            public DbSet<Task> Task { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<TaskState>(
                    b =>
                    {
                        b.HasKey(e => e.Id);
                        b.Property(e => e.Name);
                    });

                modelBuilder.Entity<Project>(
                    b =>
                    {
                        b.HasKey(e => e.Id);
                        b.Property(e => e.Code);
                        b.Property(e => e.Name);
                        b.Property(e => e.FinishDate);
                        b.Property(e => e.StartDate);
                        b.Property(e => e.ParentId);
                        b.HasMany(e => e.Tasks).WithOne(i => i.Project).IsRequired().HasForeignKey(i => i.ProjectId);
                        b.HasOne(e => e.Parent).WithMany(i => i.Children).HasForeignKey(i => i.ParentId).OnDelete(DeleteBehavior.ClientSetNull);
                    });

                modelBuilder.Entity<Task>(
                    b =>
                    {
                        b.HasKey(e => e.Id);
                        b.Property(e => e.Name);
                        b.Property(e => e.FinishDate);
                        b.Property(e => e.StartDate);
                        b.Property(e => e.StateId);
                        b.Property(e => e.ParentId);
                        b.HasOne(e => e.Project).WithMany(e => e.Tasks).HasForeignKey(i => i.ProjectId).IsRequired();
                        b.HasOne(e => e.State).WithMany(e => e.Task).HasForeignKey(i => i.StateId).IsRequired()
                            .OnDelete(DeleteBehavior.ClientSetNull);
                        //b.HasOne(e => e.Parent).WithOne().HasForeignKey<Task>(i => i.ParentId).OnDelete(DeleteBehavior.ClientSetNull);
                        b.HasMany(e => e.Children).WithOne().HasForeignKey(i => i.ParentId);
                    });



                //seed data
                modelBuilder.Entity<TaskState>().HasData(GetTaskStates());
                modelBuilder.Entity<Project>().HasData(GetProject());
                modelBuilder.Entity<Task>().HasData(GetTasks());
            }

            private Project[] GetProject()
            {
                return new Project[] {
                    new Project { Code = "PJ001", Id = 1, FinishDate = DateTime.Now.AddYears(1), Name = "Project-01", StartDate = DateTime.Now },
                    new Project { Code = "PJ002", Id = 2, FinishDate = DateTime.Now.AddYears(1).AddMonths(-3), Name = "Project-02", StartDate = DateTime.Now, ParentId = 1 }
                };
            }

            private Task[] GetTasks()
            {
                return new Task[] {
                    new Task{ Id = 1, ProjectId = 1, Name = "Task-01", StateId = 1, StartDate = DateTime.Now, FinishDate = DateTime.Now.AddMonths(1)},
                    new Task{ Id = 2, ProjectId = 1, Name = "Task-02", StateId = 2, StartDate = DateTime.Now, FinishDate = DateTime.Now.AddMonths(2), ParentId = 1},
                    new Task{ Id = 3, ProjectId = 2, Name = "Task-03", StateId = 3, StartDate = DateTime.Now, FinishDate = DateTime.Now.AddMonths(3)},
                    new Task{ Id = 4, ProjectId = 2, Name = "Task-04", StateId = 1, StartDate = DateTime.Now, FinishDate = DateTime.Now.AddMonths(4)}
                };
            }

            private TaskState[] GetTaskStates()
            {
                return new TaskState[] {
                    new TaskState{ Id = 1, Name = "Planned" },
                    new TaskState{ Id = 2, Name = "inProgress" },
                    new TaskState{ Id = 3, Name = "Completed" }
                };
            }
        }
    }
}
