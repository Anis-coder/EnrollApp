using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserCourseEnrollApp.Models
{
    public class User
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string firstName {  get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date Of Birth")]
        public string dob { get; set; }
    }
}
