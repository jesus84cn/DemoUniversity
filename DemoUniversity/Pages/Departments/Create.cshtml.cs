using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoUniversity.Models;

namespace DemoUniversity.Pages.Departments
{
    public class CreateModel : PageModel
    {
        private readonly DemoUniversity.Models.SchoolContext _context;

        public CreateModel(DemoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["InstructorID"] = new SelectList(_context.Instructors, "InstructorID", "FirstName");
            return Page();
        }

        [BindProperty]
        public Department Department { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Departments.Add(Department);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}