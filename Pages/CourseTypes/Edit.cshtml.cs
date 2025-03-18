using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CeilApp.Data;
using CeilApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CeilApp.Pages.CourseTypes
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly CeilApp.Data.ApplicationDbContext _context;

        public EditModel(CeilApp.Data.ApplicationDbContext context)
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
            CourseType = coursetype;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CourseType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseTypeExists(CourseType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CourseTypeExists(int id)
        {
            return _context.CourseTypes.Any(e => e.Id == id);
        }
    }
}