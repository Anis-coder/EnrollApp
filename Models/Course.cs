using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Query;

namespace UserCourseEnrollApp.Models
{
    public class Course
    {

        [Key]
        public int ? CourseId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string ? Description { get; set; }

        [NotMapped]
        public int[]? CourseIds { get; set; }
    }
}
