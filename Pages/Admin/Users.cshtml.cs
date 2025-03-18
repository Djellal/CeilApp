using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeilApp.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IList<UserViewModel> Users { get; set; } = new List<UserViewModel>();
        public SelectList RolesList { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string SelectedRole { get; set; }

        public async Task OnGetAsync()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            RolesList = new SelectList(roles);

            var users = await _userManager.Users.ToListAsync();
            
            var userViewModels = new List<UserViewModel>();
            
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                
                // Filter by selected role if one is specified
                if (!string.IsNullOrEmpty(SelectedRole) && !userRoles.Contains(SelectedRole))
                {
                    continue;
                }
                
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = string.Join(", ", userRoles)
                });
            }
            
            Users = userViewModels;
        }
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Roles { get; set; }
    }
}