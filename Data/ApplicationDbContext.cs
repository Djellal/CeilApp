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
    public DbSet<State> States { get; set; }
    public DbSet<Municipality> Municipalities { get; set; }
    public DbSet<CourseRegistration> CourseRegistrations { get; set; }
    public DbSet<Profession> Professions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Add a unique index on UserId and SessionId to limit registrations per session
        builder.Entity<CourseRegistration>()
            .HasIndex(r => new { r.UserId, r.SessionId })
            .HasDatabaseName("IX_CourseRegistration_UserSession");
    }
}
