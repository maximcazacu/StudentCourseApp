using System.ComponentModel.DataAnnotations;

namespace StudentCourseApp.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }
    }
}
