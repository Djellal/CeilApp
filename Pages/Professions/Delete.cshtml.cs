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
using CeilApp.Services;

namespace CeilApp.Pages.Professions
{
    [Authorize(Roles = Globals.Admin)]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Profession Profession { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Professions == null)
            {
                return NotFound();
            }

            var profession = await _context.Professions.FirstOrDefaultAsync(m => m.Id == id);

            if (profession == null)
            {
                return NotFound();
            }
            else 
            {
                Profession = profession;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Professions == null)
            {
                return NotFound();
            }
            
            var profession = await _context.Professions.FindAsync(id);

            if (profession != null)
            {
                Profession = profession;
                
                // Check if there are course registrations using this profession
                var hasRegistrations = await _context.CourseRegistrations.AnyAsync(r => r.ProfessionId == id);
                
                if (hasRegistrations)
                {
                    ModelState.AddModelError(string.Empty, "Cannot delete profession because it is being used by course registrations.");
                    return Page();
                }
                
                _context.Professions.Remove(Profession);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}