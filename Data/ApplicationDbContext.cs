using Microsoft.EntityFrameworkCore;          
using EmployeeManagement.Models;           

namespace EmployeeManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
    //It is the constructor which creates Dbcontext object of the class file ApplicationDbContext by passing paraments connection string from the program.cs file 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }                

        public DbSet<Employee> Employees { get; set; } // It creates the database Employee and table Employees using Dbset
    }
}
