using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SleekFlowToDoList2.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<TODO> TODOs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Send TODO Table
            modelBuilder.Entity<TODO>().HasData(
                new TODO
                {
                    ID = 1,
                    Name = "To Design Task",
                    Description = "To get requirement from end user and design the task",
                    DueDate = new DateTime(2023, 1, 30),
                    Status = 1
                }
                );
            modelBuilder.Entity<TODO>().HasData(
                new TODO
                {
                    ID = 2,
                    Name = "To Implement Task",
                    Description = "To implement the task based on the Designing Task",
                    DueDate = new DateTime(2023, 1, 30),
                    Status = 1
                }
                );
            modelBuilder.Entity<TODO>().HasData(
                new TODO
                {
                    ID = 3,
                    Name = "To Deploy Task",
                    Description = "To deploy the task after development task was done",
                    DueDate = new DateTime(2023, 1, 30),
                    Status = 1
                }
                );
            modelBuilder.Entity<TODO>().HasData(
                new TODO
                {
                    ID = 4,
                    Name = "To Review Task",
                    Description = "To review and monitor the performance of the program after the task has been done",
                    DueDate = new DateTime(2023, 1, 30),
                    Status = 1
                }
                );
        }
    }
}
