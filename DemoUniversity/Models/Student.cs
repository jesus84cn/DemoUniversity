using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUniversity.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }


    }
}
