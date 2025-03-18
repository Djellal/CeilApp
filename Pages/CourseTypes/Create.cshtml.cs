using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CeilApp.Data;
using CeilApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CeilApp.Pages.CourseTypes
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly CeilApp.Data.ApplicationDbContext _context;

        public CreateModel(CeilApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CourseType CourseType { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CourseTypes.Add(CourseType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}