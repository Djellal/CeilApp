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

namespace CeilApp.Pages.Courses
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly CeilApp.Data.ApplicationDbContext _context;

        public DetailsModel(CeilApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.CourseType)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (course == null)
            {
                return NotFound();
            }
            else
            {
                Course = course;
            }
            return Page();
        }
    }
}