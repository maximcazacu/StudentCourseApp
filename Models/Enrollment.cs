using System.ComponentModel.DataAnnotations;

namespace StudentCourseApp.Models
{
    public class Enrollment
    {
        public int ID { get; set; }

        [Required]
        public int StudentID { get; set; }
        public Student Student { get; set; }

        [Required]
        public int CourseID { get; set; }
        public Course Course { get; set; }

        [Range(1, 10)]
        public int? Grade { get; set; }
    }
}
