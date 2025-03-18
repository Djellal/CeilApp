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
    public class IndexModel : PageModel
    {
        private readonly CeilApp.Data.ApplicationDbContext _context;

        public IndexModel(CeilApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Course = await _context.Courses
                .Include(c => c.CourseType)
                .ToListAsync();
        }
    }
}