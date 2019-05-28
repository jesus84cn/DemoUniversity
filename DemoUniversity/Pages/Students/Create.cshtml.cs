using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoUniversity.Models;

namespace DemoUniversity.Pages.Students
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
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // USING VM MODEL
            //var entry = _context.Add(new Student());
            //entry.CurrentValues.SetValues(StudentVM);
            //await _context.SaveChangesAsync();
            //return RedirectToPage("./Index");

            //USE THIS CODE TO PREVENT OVERPOSTING

            var emptyStudent = new Student();
            if (await TryUpdateModelAsync<Student>(
                emptyStudent, "student",
                s => s.FirstName, s => s.LastName, s => s.EnrollmentDate))
            {
                _context.Student.Add(emptyStudent);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return null;


        }
    }
}