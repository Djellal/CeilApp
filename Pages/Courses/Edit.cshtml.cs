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

namespace CeilApp.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        public List<CourseLevel> CourseLevels { get; set; } = new List<CourseLevel>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            Course = course;
            
            // Load course levels
            CourseLevels = await _context.CourseLevels
                .Where(cl => cl.CourseId == id)
                .ToListAsync();
            
            ViewData["CourseTypeId"] = new SelectList(_context.CourseTypes, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(Course.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit", new { id = Course.Id });
        }

        // Add Course Level
        public async Task<IActionResult> OnPostAddCourseLevelAsync(int courseId, string name, string nameAr, int duration, string previousCourseLevelId)
        {
            if (!CourseExists(courseId))
            {
                return NotFound();
            }

            var courseLevel = new CourseLevel
            {
                CourseId = courseId,
                Name = name,
                NameAr = nameAr,
                Duration = duration,
                PreviousCourseLevelId = string.IsNullOrEmpty(previousCourseLevelId) ? null : int.Parse(previousCourseLevelId)
            };

            _context.CourseLevels.Add(courseLevel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Edit", new { id = courseId });
        }

        // Edit Course Level
        public async Task<IActionResult> OnPostEditCourseLevelAsync(int id, int courseId, string name, string nameAr, int duration, string previousCourseLevelId)
        {
            var courseLevel = await _context.CourseLevels.FindAsync(id);
            
            if (courseLevel == null)
            {
                return NotFound();
            }

            courseLevel.Name = name;
            courseLevel.NameAr = nameAr;
            courseLevel.Duration = duration;
            courseLevel.PreviousCourseLevelId = string.IsNullOrEmpty(previousCourseLevelId) ? null : int.Parse(previousCourseLevelId);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseLevelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit", new { id = courseId });
        }

        // Delete Course Level
        public async Task<IActionResult> OnPostDeleteCourseLevelAsync(int id, int courseId)
        {
            var courseLevel = await _context.CourseLevels.FindAsync(id);
            
            if (courseLevel == null)
            {
                return NotFound();
            }

            _context.CourseLevels.Remove(courseLevel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Edit", new { id = courseId });
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        private bool CourseLevelExists(int id)
        {
            return _context.CourseLevels.Any(e => e.Id == id);
        }
    }
}