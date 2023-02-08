using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB.Models;

namespace WEB.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Accounting> Accountings { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Soft> Softs { get; set; }
        public DbSet<Executor> Executors { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
