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
                Database.EnsureCreated();
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Project>(
                    b =>
                    {
                        b.HasKey(e => e.Id);
                        b.Property(e => e.Code);
                        b.Property(e => e.Name);
                        b.Property(e => e.FinishDate);
                        b.Property(e => e.StartDate);
                        b.Property(e => e.StateId);
                        b.Property(e => e.ParentId);
                        //b.HasMany(e => e.Tags).WithOne().IsRequired();
                    });
                modelBuilder.Entity<Project>().HasData(
                    new Project { Code = "PJ001", Id = 1, FinishDate = DateTime.Now.AddYears(1), Name = "Project-01", StartDate = DateTime.Now, StateId = 1 }
                    ); ;

                modelBuilder.Entity<Task>(
                    b =>
                    {
                        b.HasKey(e => e.Id);
                        b.Property(e => e.Name);
                        b.Property(e => e.FinishDate);
                        b.Property(e => e.StartDate);
                        b.Property(e => e.StateId);
                        b.Property(e => e.ParentId);
                    });
            }

            //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            //{
            //    //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=Pms;");
            //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Pms;Trusted_Connection=True;");
            //}
        }
    }
}
