using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CeilApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Navigation property for course registrations
        public virtual ICollection<CourseRegistration>? CourseRegistrations { get; set; }
    }
}