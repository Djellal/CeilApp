using CeilApp.Data;
using CeilApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CeilApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Session CurrentSession { get; set; }
        public bool IsRegistrationOpen { get; set; }

        public async Task OnGetAsync()
        {
            // Get the application settings to determine current session and registration status
            var appSettings = await _context.AppSettings.FirstOrDefaultAsync();
            
            if (appSettings != null && appSettings.CurrentSessionId.HasValue)
            {
                // Get the current session details
                CurrentSession = await _context.Sessions.FindAsync(appSettings.CurrentSessionId.Value);
                IsRegistrationOpen = appSettings.IsRegistrationOpened;
            }
        }
    }
}
