using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoUniversity.Models
{
    public class Instructor
    {
        public int InstructorID { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(30)]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public ICollection<CourseAssignment> CourseAssignments { get; set; }

        public OfficeAssignment OfficeAssignment { get; set; }

    }
}
