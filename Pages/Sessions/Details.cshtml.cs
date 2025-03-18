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

namespace CeilApp.Pages.Sessions
{
    [Authorize(Roles = Services.Globals.Admin)]
    public class DetailsModel : PageModel
    {
        private readonly CeilApp.Data.ApplicationDbContext _context;

        public DetailsModel(CeilApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Session Session { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FirstOrDefaultAsync(m => m.Id == id);

            if (session is not null)
            {
                Session = session;

                return Page();
            }

            return NotFound();
        }
    }
}
