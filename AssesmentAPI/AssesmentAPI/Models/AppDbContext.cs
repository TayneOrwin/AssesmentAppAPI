using System;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace AssesmentAPI.Models

{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


  





        public DbSet<Models.Entities.Employee> Employees { get; set; }


        public DbSet<Models.Entities.AccessRole> accessRoles { get; set; }

        public DbSet<Models.Entities.Manager> managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);





        }



    }
}

