using System.ComponentModel.DataAnnotations;

namespace StudentManagementAPI.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string RollNo { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public string Department { get; set; }

        public int Semester { get; set; }
    }
}