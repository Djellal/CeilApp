using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CeilApp.Models
{
    public class Municipality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [MaxLength(250)]
        public string Name { get; set; } = "";

        [MaxLength(250)]
        public string NameAr { get; set; } = "";


        [ForeignKey("State")]
        public string? StateId { get; set; }
        public State? State { get; set; }
    }
}
