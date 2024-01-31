using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserCourseEnrollApp.Models
{
    public class EnrollMent
    {
        [Key]
        public int? Id { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public string StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public string ? EndDate { get; set; }

        [NotMapped]
        public int CourseId { get; set; }

        [NotMapped]
        public int[]? CourseIds { get; set; }

        [Required]
        public int UserId { get; set; }

        [NotMapped]
        public List<Course> ? Courses { get; set; }

        [NotMapped]
        public List<User> ? Users { get; set; }
    }
}
