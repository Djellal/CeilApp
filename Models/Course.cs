using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CeilApp.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Code { get; set; } = "";

        [MaxLength(250)]
        public string Name { get; set; } = "";

        [MaxLength(250)]
        public string NameAr { get; set; } = "";

        public int Duration { get; set; } = 0;

        [ForeignKey("CourseType")]
        public int CourseTypeId { get; set; }
        public CourseType? CourseType { get; set; }
    }
}