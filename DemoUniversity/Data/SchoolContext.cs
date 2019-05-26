using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DemoUniversity.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<DemoUniversity.Models.Student> Student { get; set; }
        public DbSet<DemoUniversity.Models.Enrollment> Enrollment{ get; set; }
        public DbSet<DemoUniversity.Models.Course> Course { get; set; }
    }
}
