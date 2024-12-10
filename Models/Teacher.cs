using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentCourseApp.Models
{
    public class Teacher
    {
        public int ID { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string AcademicRank { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}

