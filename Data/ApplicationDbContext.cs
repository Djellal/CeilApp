using CeilApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CeilApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Session> Sessions { get; set; }
    public DbSet<AppSetting> AppSettings { get; set; }
    public DbSet<CourseType> CourseTypes { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseLevel> CourseLevels { get; set; }

}
