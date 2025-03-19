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

namespace CeilApp.Pages.States
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
        public State State { get; set; } = default!;
        public int MunicipalityCount { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States.FirstOrDefaultAsync(m => m.Id == id);

            if (state == null)
            {
                return NotFound();
            }
            else 
            {
                State = state;
                MunicipalityCount = await _context.Municipalities.CountAsync(m => m.StateId == id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }
            
            var state = await _context.States.FindAsync(id);

            if (state != null)
            {
                State = state;
                
                // Check if there are municipalities associated with this state
                var hasMunicipalities = await _context.Municipalities.AnyAsync(m => m.StateId == id);
                
                if (hasMunicipalities)
                {
                    ModelState.AddModelError(string.Empty, "Cannot delete state because it has municipalities associated with it. Please delete the municipalities first.");
                    MunicipalityCount = await _context.Municipalities.CountAsync(m => m.StateId == id);
                    return Page();
                }
                
                _context.States.Remove(State);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}