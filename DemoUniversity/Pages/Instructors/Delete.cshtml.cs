using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoUniversity.Models;

namespace DemoUniversity.Pages.Instructors
{
    public class DeleteModel : PageModel
    {
        private readonly DemoUniversity.Models.SchoolContext _context;

        public DeleteModel(DemoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Instructor Instructor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.InstructorID == id);

            if (Instructor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Instructor instructorToRemove = await _context.Instructors
                .Include(i => i.CourseAssignments)
                .SingleAsync(i => i.InstructorID == id);

            var departments = await _context.Departments
                .Where(d => d.InstructorID == id)
                .ToListAsync();
            departments.ForEach(d => d.InstructorID = null);
            _context.Instructors.Remove(instructorToRemove);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
