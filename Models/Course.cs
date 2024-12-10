using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentCourseApp.Models
{
    public class Course
    {
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Range(1, 30)]
        public int Credits { get; set; }

        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
