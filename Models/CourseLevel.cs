using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CeilApp.Models
{
    public class CourseLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [MaxLength(250)]
        public string Name { get; set; } = "";

        [MaxLength(250)]
        public string NameAr { get; set; } = "";

        public int Duration { get; set; } = 0;

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }


        [ForeignKey("PrevCourseLevel")]
        public int? PreviousCourseLevelId { get; set; }
        public CourseLevel? PrevCourseLevel { get; set; }
    }
}
