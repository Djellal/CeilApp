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

namespace CeilApp.Pages.AppSettings
{
    [Authorize(Roles = Services.Globals.Admin)]
    public class IndexModel : PageModel
    {
        private readonly CeilApp.Data.ApplicationDbContext _context;

        public IndexModel(CeilApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppSetting AppSetting { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            
            var appsetting =  await _context.AppSettings.FirstOrDefaultAsync();
            if (appsetting == null)
            {
                return NotFound();
            }
            AppSetting = appsetting;
           ViewData["CurrentSessionId"] = new SelectList(_context.Sessions, "Id", "SessionName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AppSetting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppSettingExists(AppSetting.Id))
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

        private bool AppSettingExists(int id)
        {
            return _context.AppSettings.Any(e => e.Id == id);
        }
    }
}
