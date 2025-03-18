using CeilApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CeilApp.ViewComponents
{
    public class AppSettingsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AppSettingsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var appSettings = await _context.AppSettings.FirstOrDefaultAsync();
            ViewBag.AppSettings = appSettings;
            return View();
        }
    }
}