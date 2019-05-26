using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoUniversity.Models;

namespace DemoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly DemoUniversity.Models.SchoolContext _context;

        public IndexModel(DemoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }

        public string DateSort { get; set; }

        public string CurrentSort { get; set; }

        public string CurrentFilter { get; set; }

        public PaginatedList<Student> Student { get;set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date" ;
            CurrentFilter = searchString;

            if(searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<Student> studentIQ = from s in _context.Student
                                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                studentIQ = studentIQ.Where(s => s.LastName.Contains(searchString) 
                                                || s.FirstName.Contains(searchString));
                // Can use ToUpper() method to explicitly make it case-insensetive, but hits performance.
            }

            switch (sortOrder)
            {
                case "name_desc":
                    studentIQ = studentIQ.OrderByDescending(s => s.LastName);
                    break;

                case "Date":
                    studentIQ = studentIQ.OrderBy(s => s.EnrollmentDate);
                    break;

                case "date_desc":
                    studentIQ = studentIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;

                default:
                    studentIQ = studentIQ.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 4;

            Student = await PaginatedList<Student>.CreateAsync(studentIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
