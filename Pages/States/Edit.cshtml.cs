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
using CeilApp.Services;

namespace CeilApp.Pages.States
{
    [Authorize(Roles = Globals.Admin)]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(State).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(State.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit", new { id = State.Id });
        }
        
        // Add Municipality
        public async Task<IActionResult> OnPostAddMunicipalityAsync(string stateId, string code, string name, string nameAr)
        {
            if (!StateExists(stateId))
            {
                return NotFound();
            }

            var municipality = new Municipality
            {               
                Name = name,
                NameAr = nameAr,
                StateId = stateId
            };

            _context.Municipalities.Add(municipality);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Edit", new { id = stateId });
        }
        
        // Edit Municipality
        public async Task<IActionResult> OnPostEditMunicipalityAsync(int id, string stateId, string code, string name, string nameAr)
        {
            var municipality = await _context.Municipalities.FindAsync(id);
            
            if (municipality == null)
            {
                return NotFound();
            }
                       
            municipality.Name = name;
            municipality.NameAr = nameAr;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunicipalityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit", new { id = stateId });
        }
        
        // Delete Municipality
        public async Task<IActionResult> OnPostDeleteMunicipalityAsync(int id, string stateId)
        {
            var municipality = await _context.Municipalities.FindAsync(id);
            
            if (municipality == null)
            {
                return NotFound();
            }

            _context.Municipalities.Remove(municipality);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Edit", new { id = stateId });
        }

        private bool StateExists(string id)
        {
            return _context.States.Any(e => e.Id == id);
        }
        
        private bool MunicipalityExists(int id)
        {
            return _context.Municipalities.Any(e => e.Id == id);
        }
    }
}