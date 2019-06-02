using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoUniversity.Models;

namespace DemoUniversity.Pages.Departments
{
    public class DeleteModel : PageModel
    {
        private readonly DemoUniversity.Models.SchoolContext _context;

        public DeleteModel(DemoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Department Department { get; set; }

        public string ConcurrencyErrorMessage { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id, bool? concurrencyError)
        {
            Department = await _context.Departments
                .Include(i => i.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartmentID == id);

            if(Department == null)
            {
                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ConcurrencyErrorMessage = "The record you are trying to delete was modified by another user after you have clicked delete." 
                   + "The delete operation has been canceled and current values in database have been displayed." 
                   + "If you still want to delete this record click delete button again.";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                if(await _context.Departments.AnyAsync(
                    m => m.DepartmentID == id))
                {
                    _context.Departments.Remove(Department);
                    await _context.SaveChangesAsync();
                }
                return RedirectToPage("./Index");
            }
            catch(DbUpdateConcurrencyException)
            {
                return RedirectToPage("./Delete", new { concurrencyError = true, id = id });
            }
        }
    }
}
