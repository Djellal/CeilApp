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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<State> States { get;set; } = default!;

        public async Task OnGetAsync()
        {
            States = await _context.States.ToListAsync();
        }
    }
}