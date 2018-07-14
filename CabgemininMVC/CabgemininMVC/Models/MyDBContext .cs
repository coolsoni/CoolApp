using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CabgemininMVC.Models
{
    public class MyDBContext : DbContext
    {

        public MyDBContext()
        : base("DefaultConnection")
    {
        }
        public DbSet<BranchModel> branch { get; set; }
        public DbSet<DepartmentModel> deptt { get; set; }
        public DbSet<EmployeeModel> employee { get; set; }
        public DbSet<SectionModel> section { get; set; }

        public DbSet<StudentModel> student { get; set; }

    }
}