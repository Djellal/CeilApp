using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CeilApp.Data;
using CeilApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CeilApp.Pages.CourseTypes
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly CeilApp.Data.ApplicationDbContext _context;

        public DeleteModel(CeilApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CourseType CourseType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursetype = await _context.CourseTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (coursetype == null)
            {
                return NotFound();
            }
            else
            {
                CourseType = coursetype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursetype = await _context.CourseTypes.FindAsync(id);
            if (coursetype != null)
            {
                CourseType = coursetype;
                _context.CourseTypes.Remove(CourseType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}