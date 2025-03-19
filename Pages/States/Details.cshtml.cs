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
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public State State { get; set; } = default!;
        public List<Municipality> Municipalities { get; set; } = new List<Municipality>();

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
            
            State = state;
            
            // Load municipalities for this state
            Municipalities = await _context.Municipalities
                .Where(m => m.StateId == id)
                .ToListAsync();
                
            return Page();
        }
    }
}