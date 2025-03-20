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
using Microsoft.AspNetCore.Http; // Ensure this is included
using System.IO; // Ensure this is included
using Microsoft.AspNetCore.Hosting;

namespace CeilApp.Pages.AppSettings
{
    [Authorize(Roles = Services.Globals.Admin)]
    public class IndexModel : PageModel
    {
        private readonly CeilApp.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public IndexModel(CeilApp.Data.ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public AppSetting AppSetting { get; set; } = default!;

        [BindProperty]
        public IFormFile LogoFile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var appsetting = await _context.AppSettings.FirstOrDefaultAsync();
            if (appsetting == null)
            {
                return NotFound();
            }
            AppSetting = appsetting;
            ViewData["CurrentSessionId"] = new SelectList(_context.Sessions, "Id", "SessionName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Handle logo file upload
            if (LogoFile != null)
            {
                // Create uploads directory if it doesn't exist
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate unique filename
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + LogoFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await LogoFile.CopyToAsync(fileStream);
                }

                // Update the logo path in the database
                AppSetting.Logo = "/uploads/" + uniqueFileName;
            }

            _context.Attach(AppSetting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Update global settings
                Services.Globals.appSettings = new AppSetting { 
                    Address = AppSetting.Address,
                    CurrentSession = AppSetting.CurrentSession,
                    CurrentSessionId = AppSetting.CurrentSessionId,
                    Id = AppSetting.Id,
                    IsRegistrationOpened = AppSetting.IsRegistrationOpened,
                    OrganizationName = AppSetting.OrganizationName,
                    Logo = AppSetting.Logo,
                    WebSite = AppSetting.WebSite,
                    FB = AppSetting.FB,
                    LinkredIn = AppSetting.LinkredIn,
                    Email = AppSetting.Email,
                    Tel = AppSetting.Tel
                };
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
