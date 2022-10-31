using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Context
{
    public class MyContext : DbContext
    {
        public MyContext()
        {

        }
        public MyContext(DbContextOptions<MyContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees{ get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
