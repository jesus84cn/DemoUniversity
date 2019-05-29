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
    public class DetailsModel : PageModel
    {
        private readonly DemoUniversity.Models.SchoolContext _context;

        public DetailsModel(DemoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

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
    }
}
